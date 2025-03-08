using Anvil.Services;
using System.Collections.Generic;
using System.Linq;
using Anvil.API;
using XM.Inventory;
using XM.Progression.Skill;
using XM.Shared.Core;
using XM.Shared.Core.Localization;
using XM.UI;
using Action = System.Action;

namespace XM.Progression.Craft.UI
{
    [ServiceBinding(typeof(IViewModel))]
    [ServiceBinding(typeof(IRefreshable))]
    internal class CraftViewModel: 
        ViewModel<CraftViewModel>, 
        IRefreshable
    {
        private int _currentRecipeIndex;
        private readonly List<RecipeType> _recipeTypes = new();
        private const int RecordsPerPage = 40;
        private bool _skipPaginationSearch;
        private SkillType _skill;

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
                _currentRecipeIndex = -1;

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
            _currentRecipeIndex = -1;

            LoadCategories();
            Search();

            WatchOnClient(model => model.SearchText);
            WatchOnClient(model => model.SelectedCategoryId);
            WatchOnClient(model => model.SelectedPageIndex);
            _skipPaginationSearch = false;
        }

        private void LoadCategories()
        {
            if (Categories != null)
            {
                Categories.Clear();
                Categories.Add(new NuiComboEntry("Select...", 0));
                return;
            }

            var categories = new XMBindingList<NuiComboEntry>();

            categories.Add(new NuiComboEntry("Select...", 0));
            foreach (var (type, detail) in Craft.GetRecipeCategoriesBySkill(_skill))
            {
                categories.Add(new NuiComboEntry(detail.Name.ToLocalizedString(), (int)type));
            }

            Categories = categories;
        }

        private void Search()
        {
            Dictionary<RecipeType, RecipeDetail> recipes;

            // Category selected
            if (SelectedCategoryId > 0)
            {
                var category = (RecipeCategoryType)SelectedCategoryId;
                recipes = Craft.GetRecipesBySkillAndCategory(_skill, category);
            }
            // Neither filters selected
            else
            {
                recipes = Craft.GetAllRecipes();
            }

            // Search text filter
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                recipes = recipes
                    .Where(x =>
                        ItemCache.GetItemNameByResref(x.Value.Resref)
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
            var recipeToggles = new XMBindingList<bool>();
            _recipeTypes.Clear();

            foreach (var (type, detail) in recipes)
            {
                if (Craft.CanPlayerCraftRecipe(Player, type))
                {
                    var name = $"{ItemCache.GetItemNameByResref(detail.Resref)} [{LocaleString.Lv.ToLocalizedString()}. {detail.Level}]";

                    recipeNames.Add(name);
                    recipeToggles.Add(false);
                    _recipeTypes.Add(type);
                }
            }

            RecipeNames = recipeNames;
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
            pageNumbers.Add(new NuiComboEntry($"Page 1", 0));
            for (var x = 2; x <= pages; x++)
            {
                pageNumbers.Add(new NuiComboEntry($"Page {x}", x - 1));
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

            _currentRecipeIndex = -1;
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

            _currentRecipeIndex = -1;
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

            _currentRecipeIndex = -1;
            SelectedPageIndex = newPage;
            Search();
            _skipPaginationSearch = false;
        };

        public Action OnSelectRecipe() => () =>
        {
            // Deselect the current recipe.
            if (_currentRecipeIndex > -1)
            {
                RecipeToggles[_currentRecipeIndex] = false;
            }

            _currentRecipeIndex = NuiGetEventArrayIndex();
            RecipeToggles[_currentRecipeIndex] = true;
            LoadRecipeDetail();
        };

        public Action OnClickCraft() => () =>
        {
        };

        private void DisplayRecipeDetail(RecipeType recipe)
        {
            var detail = Craft.GetRecipe(recipe);
            var itemName = ItemCache.GetItemNameByResref(detail.Resref);

            RecipeName = $"Recipe: {detail.Quantity}x {itemName}";
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
            _currentRecipeIndex = -1;
        }

        private void LoadRecipeDetail()
        {
            if (_currentRecipeIndex > -1)
            {
                var selectedRecipe = _recipeTypes[_currentRecipeIndex];
                DisplayRecipeDetail(selectedRecipe);
            }
            else
            {
                ClearRecipeDetail();
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
