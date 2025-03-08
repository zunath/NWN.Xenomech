using System;
using Anvil.Services;
using System.Collections.Generic;
using System.Linq;
using Anvil.API;
using XM.Inventory;
using XM.Progression.Skill;
using XM.Shared.API.Constants;
using XM.Shared.API.NWNX.ObjectPlugin;
using XM.Shared.Core;
using XM.Shared.Core.Activity;
using XM.Shared.Core.Localization;
using XM.UI;
using Action = System.Action;
using XM.Shared.API.NWNX.PlayerPlugin;

namespace XM.Progression.Craft.UI
{
    [ServiceBinding(typeof(IViewModel))]
    [ServiceBinding(typeof(IRefreshable))]
    internal class CraftViewModel: 
        ViewModel<CraftViewModel>, 
        IRefreshable
    {
        private static readonly Color _red = new(255, 0, 0);
        private static readonly Color _green = new(0, 255, 0);

        private int _selectedRecipeIndex;
        private readonly List<RecipeType> _recipeTypes = new();
        private const int RecordsPerPage = 40;
        private bool _skipPaginationSearch;
        private SkillType _skill;
        private const float CraftDelaySeconds = 6f;
        private const int BaseSuccessRate = 65;

        private readonly List<string> _components = new();

        [Inject]
        public ActivityService Activity { get; set; }

        [Inject]
        public SkillService Skill { get; set; }

        [Inject]
        public CraftService Craft { get; set; }

        [Inject]
        public ItemCacheService ItemCache { get; set; }

        public string Title
        {
            get => Get<string>();
            set => Set(value);
        }

        public string SearchText
        {
            get => Get<string>();
            set => Set(value);
        }

        public int SelectedPageIndex
        {
            get => Get<int>();
            set
            {
                Set(value);

                if (!_skipPaginationSearch)
                    Search();
            }
        }

        public XMBindingList<NuiComboEntry> PageNumbers
        {
            get => Get<XMBindingList<NuiComboEntry>>();
            set => Set(value);
        }

        public int SelectedCategoryId
        {
            get => Get<int>();
            set
            {
                Set(value);
                _selectedRecipeIndex = -1;

                if (!_skipPaginationSearch)
                    Search();
            }
        }

        public XMBindingList<string> RecipeNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<bool> RecipeToggles
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }

        public XMBindingList<NuiComboEntry> Categories
        {
            get => Get<XMBindingList<NuiComboEntry>>();
            set => Set(value);
        }

        public XMBindingList<Color> RecipeColors
        {
            get => Get<XMBindingList<Color>>();
            set => Set(value);
        }

        public string RecipeName
        {
            get => Get<string>();
            set => Set(value);
        }

        public string RecipeLevel
        {
            get => Get<string>();
            set => Set(value);
        }
        public bool CanCraftRecipe
        {
            get => Get<bool>();
            set => Set(value);
        }

        public XMBindingList<string> RecipeDetails
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public XMBindingList<Color> RecipeDetailColors
        {
            get => Get<XMBindingList<Color>>();
            set => Set(value);
        }

        public override void OnOpen()
        {
            _skipPaginationSearch = true;

            var payload = GetInitialData<CraftPayload>();
            _skill = payload.Skill;
            var definition = Skill.GetCraftSkillDefinition(_skill);
            Title = definition.DeviceName.ToLocalizedString();


            RecipeName = string.Empty;
            RecipeLevel = string.Empty;
            SearchText = string.Empty;
            SelectedPageIndex = 0;
            SelectedCategoryId = 0;
            _selectedRecipeIndex = -1;

            LoadCategories();
            Search();

            WatchOnClient(model => model.SearchText);
            WatchOnClient(model => model.SelectedCategoryId);
            WatchOnClient(model => model.SelectedPageIndex);
            _skipPaginationSearch = false;
        }

        private void LoadCategories()
        {
            var selectText = LocaleString.Select.ToLocalizedString() + "...";

            if (Categories != null)
            {
                Categories.Clear();
                Categories.Add(new NuiComboEntry(selectText, 0));
                return;
            }

            var categories = new XMBindingList<NuiComboEntry>();

            categories.Add(new NuiComboEntry(selectText, 0));
            foreach (var (type, detail) in Craft.GetRecipeCategoriesBySkill(_skill))
            {
                categories.Add(new NuiComboEntry(detail.Name.ToLocalizedString(), (int)type));
            }

            Categories = categories;
        }

        private void Search()
        {
            Dictionary<RecipeType, RecipeDetail> recipes;

            if (SelectedCategoryId > 0)
            {
                var category = (RecipeCategoryType)SelectedCategoryId;
                recipes = Craft.GetRecipesBySkillAndCategory(_skill, category);
            }
            else
            {
                recipes = Craft.GetAllRecipesBySkill(_skill);
            }

            // Search text filter
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                recipes = recipes
                    .Where(x =>
                        ItemCache.GetItemNameByResref(x.Value.Items[RecipeQualityType.Normal].Resref)
                            .ToLower()
                            .Contains(SearchText.ToLower()))
                    .ToDictionary(x => x.Key, y => y.Value);
            }

            UpdatePagination(recipes.Count);

            recipes = recipes
                .Skip(SelectedPageIndex * RecordsPerPage)
                .Take(RecordsPerPage)
                .ToDictionary(x => x.Key, y => y.Value);

            var recipeNames = new XMBindingList<string>();
            var recipeColors = new XMBindingList<Color>();
            var recipeToggles = new XMBindingList<bool>();
            _recipeTypes.Clear();

            foreach (var (type, detail) in recipes)
            {
                if (Craft.CanPlayerCraftRecipe(Player, type))
                {
                    var name = $"{ItemCache.GetItemNameByResref(detail.Items[RecipeQualityType.Normal].Resref)} [{LocaleString.Lv.ToLocalizedString()} {detail.Level}]";
                    var canCraft = Craft.CanPlayerCraftRecipe(Player, type);

                    recipeNames.Add(name);
                    recipeColors.Add(canCraft ? _green : _red);
                    recipeToggles.Add(false);
                    _recipeTypes.Add(type);
                }
            }

            RecipeNames = recipeNames;
            RecipeColors = recipeColors;
            RecipeToggles = recipeToggles;

            LoadRecipeDetail();
        }

        private void UpdatePagination(int totalRecordCount)
        {
            _skipPaginationSearch = true;
            var pageNumbers = new XMBindingList<NuiComboEntry>();
            var pages = (int)(totalRecordCount / RecordsPerPage + (totalRecordCount % RecordsPerPage == 0 ? 0 : 1));

            // Always add page 1. In the event no recipes are found,
            // it still needs to be displayed.
            pageNumbers.Add(new NuiComboEntry(LocaleString.PageX.ToLocalizedString(1), 0));
            for (var x = 2; x <= pages; x++)
            {
                pageNumbers.Add(new NuiComboEntry(LocaleString.PageX.ToLocalizedString(x), x - 1));
            }

            PageNumbers = pageNumbers;

            // In the event no results are found, default the index to zero
            if (pages <= 0)
                SelectedPageIndex = 0;
            // Otherwise, if current page is outside the new page bounds,
            // set it to the last page in the list.
            else if (SelectedPageIndex > pages - 1)
                SelectedPageIndex = pages - 1;

            _skipPaginationSearch = false;

            _selectedRecipeIndex = -1;
            LoadRecipeDetail();
        }
        public Action OnClickClearSearch() => () =>
        {
            SearchText = string.Empty;
            Search();
        };

        public Action OnClickSearch() => Search;


        public Action OnClickPreviousPage() => () =>
        {
            _skipPaginationSearch = true;
            var newPage = SelectedPageIndex - 1;
            if (newPage < 0)
                newPage = 0;

            _selectedRecipeIndex = -1;
            SelectedPageIndex = newPage;
            Search();
            _skipPaginationSearch = false;
        };

        public Action OnClickNextPage() => () =>
        {
            _skipPaginationSearch = true;
            var newPage = SelectedPageIndex + 1;
            if (newPage > PageNumbers.Count - 1)
                newPage = PageNumbers.Count - 1;

            _selectedRecipeIndex = -1;
            SelectedPageIndex = newPage;
            Search();
            _skipPaginationSearch = false;
        };

        public Action OnSelectRecipe() => () =>
        {
            // Deselect the current recipe.
            if (_selectedRecipeIndex > -1)
            {
                RecipeToggles[_selectedRecipeIndex] = false;
            }

            _selectedRecipeIndex = NuiGetEventArrayIndex();
            RecipeToggles[_selectedRecipeIndex] = true;
            LoadRecipeDetail();
        };

        private List<uint> GetComponents()
        {
            var components = new List<uint>();
            var recipe = _recipeTypes[_selectedRecipeIndex];
            var detail = Craft.GetRecipe(recipe);

            for (var item = GetFirstItemInInventory(Player); GetIsObjectValid(item); item = GetNextItemInInventory(Player))
            {
                var resref = GetResRef(item);
                if (detail.Components.ContainsKey(resref))
                    components.Add(item);
            }

            return components;
        }

        private bool ProcessComponents()
        {
            var components = GetComponents();
            var aggregateList = AggregateComponents(components);
            if (aggregateList.Count <= 0)
            {
                var message = LocaleString.MissingComponents.ToLocalizedString();
                SendMessageToPC(Player, ColorToken.Red(message));
                return false;
            }

            return true;
        }
        private List<uint> AggregateComponents(List<uint> components)
        {
            var recipeType = _recipeTypes[_selectedRecipeIndex];
            var recipe = Craft.GetRecipe(recipeType);
            var remainingComponents = recipe.Components.ToDictionary(x => x.Key, y => y.Value);
            var result = new List<uint>();

            for (var index = components.Count - 1; index >= 0; index--)
            {
                var component = components[index];
                var resref = GetResRef(component);

                // Recipe does not need any more of this component type.
                if (!remainingComponents.ContainsKey(resref))
                    continue;

                var quantity = GetItemStackSize(component);

                // Player's component stack size is greater than the amount required.
                if (quantity > remainingComponents[resref])
                {
                    var originalStackSize = GetItemStackSize(component);
                    SetItemStackSize(component, remainingComponents[resref]);
                    _components.Add(ObjectPlugin.Serialize(component));
                    var reducedStackSize = originalStackSize - remainingComponents[resref];
                    SetItemStackSize(component, reducedStackSize);
                    result.Add(component);
                    remainingComponents[resref] = 0;
                }
                // Player's component stack size is less than or equal to the amount required.
                else if (quantity <= remainingComponents[resref])
                {
                    remainingComponents[resref] -= quantity;
                    _components.Add(ObjectPlugin.Serialize(component));
                    result.Add(component);
                    DestroyObject(component);
                }

                if (remainingComponents[resref] <= 0)
                    remainingComponents.Remove(resref);
            }

            var hasAllComponents = remainingComponents.Count <= 0;

            // If we're missing some components, clear the serialized component list and the result list.
            if (!hasAllComponents)
            {
                DelayCommand(0.1f, () =>
                {
                    foreach (var component in _components)
                    {
                        var item = ObjectPlugin.Deserialize(component);
                        ObjectPlugin.AcquireItem(Player, item);
                    }

                    _components.Clear();
                });

                result.Clear();
            }

            return result;
        }

        private int CalculateSuccessRate()
        {
            var skill = Skill.GetCraftSkillLevel(Player, _skill);
            var recipeType = _recipeTypes[_selectedRecipeIndex];
            var recipe = Craft.GetRecipe(recipeType);
            var delta = skill - recipe.Level;
            var rate = BaseSuccessRate + (2.5f * delta);
            rate += (int)(XMRandom.NextFloat() * 14 - 7);
            rate = Math.Clamp(rate, 5, 95);

            return (int)rate;
        }

        private void PerformCraft()
        {
            var rate = CalculateSuccessRate();
            var roll = XMRandom.D100(1);
            if (roll > rate)
            {
                ProcessFailure();
                return;
            }

            ProcessSuccess(rate);
        }

        private void ProcessSuccess(int rate)
        {
            var recipeType = _recipeTypes[_selectedRecipeIndex];
            var recipe = Craft.GetRecipe(recipeType);
            var hqChance = 0;
            var ultraChance = 0;
            var normalItem = recipe.Items[RecipeQualityType.Normal];
            var resref = normalItem.Resref;
            var quantity = normalItem.Quantity;

            if (recipe.Items.ContainsKey(RecipeQualityType.HQ) &&
                !string.IsNullOrWhiteSpace(recipe.Items[RecipeQualityType.HQ].Resref) &&
                rate > 75)
            {
                hqChance = rate - 75;
            }

            if (recipe.Items.ContainsKey(RecipeQualityType.Ultra) &&
                !string.IsNullOrWhiteSpace(recipe.Items[RecipeQualityType.Ultra].Resref) &&
                rate > 90)
            {
                ultraChance = rate - 90;
            }

            if (XMRandom.D100(1) <= ultraChance)
            {
                var ultraItem = recipe.Items[RecipeQualityType.Ultra];
                resref = ultraItem.Resref;
                quantity = ultraItem.Quantity;
            }
            else if (XMRandom.D100(1) <= hqChance)
            {
                var hqItem = recipe.Items[RecipeQualityType.HQ];
                resref = hqItem.Resref;
                quantity = hqItem.Quantity;
            }

            _components.Clear();

            CreateItemOnObject(resref, Player, quantity);

            if (Craft.CanPlayerSkillUp(Player, recipeType))
            {
                var chance = Craft.CalculateSkillUpChance(Player, recipeType);
                if (XMRandom.D100(1) <= chance)
                {
                    Craft.LevelUpCraftSkill(Player, _skill);
                }
            }

            var itemName = ItemCache.GetItemNameByResref(resref);
            var message = LocaleString.YouCreateXxY.ToLocalizedString(quantity, itemName);
            Messaging.SendMessageNearbyToPlayers(Player, message);
        }

        private void ProcessFailure()
        {
            const int ChanceToLoseItem = 65;
            const int ChanceToSkillUpOnFailure = 1;

            var recipeType = _recipeTypes[_selectedRecipeIndex];
            var recipe = Craft.GetRecipe(recipeType);
            var normalItem = recipe.Items[RecipeQualityType.Normal];
            var itemName = ItemCache.GetItemNameByResref(normalItem.Resref);

            // Process components
            foreach (var serialized in _components)
            {
                if (XMRandom.D100(1) > ChanceToLoseItem)
                {
                    var item = ObjectPlugin.Deserialize(serialized);
                    ObjectPlugin.AcquireItem(Player, item);
                }
            }

            _components.Clear();

            var message = ColorToken.Red(LocaleString.FailedToCraftTheItem.ToLocalizedString());
            SendMessageToPC(Player, message);

            if (Craft.CanPlayerSkillUp(Player, recipeType) &&
                XMRandom.D100(1) <= ChanceToSkillUpOnFailure)
            {
                Craft.LevelUpCraftSkill(Player, _skill);
            }
        }

        public Action OnClickCraft() => () =>
        {
            if (Activity.IsBusy(Player))
            {
                SendMessageToPC(Player, LocaleString.YouAreBusy.ToLocalizedString());
                return;
            }

            if (!Craft.CanPlayerCraftRecipe(Player, _recipeTypes[_selectedRecipeIndex]))
            {
                var message = LocaleString.YouCannotCraftThatItem.ToLocalizedString();
                SendMessageToPC(Player, ColorToken.Red(message));
                return;
            }

            if (ProcessComponents())
            {
                ApplyEffectToObject(DurationType.Temporary, EffectCutsceneImmobilize(), Player, CraftDelaySeconds);
                PlayerPlugin.StartGuiTimingBar(Player, CraftDelaySeconds);
                Activity.SetBusy(Player, ActivityStatusType.Crafting);
                AssignCommand(Player, () => ActionPlayAnimation(AnimationType.LoopingGetMid, 1f, CraftDelaySeconds+0.1f));

                DelayCommand(CraftDelaySeconds, () =>
                {
                    Activity.ClearBusy(Player);
                    PerformCraft();
                });

            }
        };

        private void DisplayRecipeDetail(RecipeType recipe)
        {
            var detail = Craft.GetRecipe(recipe);
            var normalItem = detail.Items[RecipeQualityType.Normal];
            var itemName = ItemCache.GetItemNameByResref(normalItem.Resref);

            RecipeName = $"Recipe: {normalItem.Quantity}x {itemName}";
            RecipeLevel = $"Level: {detail.Level}";
            var (recipeDetails, recipeDetailColors) = Craft.BuildRecipeDetail(Player, recipe);

            RecipeDetails = recipeDetails;
            RecipeDetailColors = recipeDetailColors;
        }

        private void ClearRecipeDetail()
        {
            RecipeName = string.Empty;
            RecipeLevel = string.Empty;
            RecipeDetails = new XMBindingList<string>();
            RecipeDetailColors = new XMBindingList<Color>();
            _selectedRecipeIndex = -1;
        }

        private void LoadRecipeDetail()
        {
            if (_selectedRecipeIndex > -1)
            {
                var selectedRecipe = _recipeTypes[_selectedRecipeIndex];
                DisplayRecipeDetail(selectedRecipe);

                var recipeType = _recipeTypes[_selectedRecipeIndex];
                CanCraftRecipe = Craft.CanPlayerCraftRecipe(Player, recipeType);
            }
            else
            {
                ClearRecipeDetail();
                CanCraftRecipe = false;
            }
        }

        public override void OnClose()
        {
        }

        public void Refresh()
        {
            Search();
        }
    }
}
