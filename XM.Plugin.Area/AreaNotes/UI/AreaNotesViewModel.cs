using System;
using System.Collections.Generic;
using System.Linq;
using Anvil.Services;
using XM.Shared.Core.Entity;
using XM.Shared.Core;
using XM.Shared.Core.Caching;
using XM.Shared.Core.Data;
using XM.UI;

namespace XM.Plugin.Area.AreaNotes.UI
{
    internal class AreaNotesViewModel : ViewModel<AreaNotesViewModel>
    {
        public const int MaxNoteLength = 10000;

        private readonly List<uint> _areas = new();
        private bool _isLoadingNote;

        [Inject]
        public DBService DB { get; set; }

        [Inject]
        public AreaCacheService AreaCache { get; set; }

        public string SearchText
        {
            get => Get<string>();
            set => Set(value);
        }
        public bool IsSaveEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }
        public bool IsDeleteEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public XMBindingList<bool> AreaToggled
        {
            get => Get<XMBindingList<bool>>();
            set => Set(value);
        }
        public XMBindingList<string> AreaResrefs
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }
        public XMBindingList<string> AreaNames
        {
            get => Get<XMBindingList<string>>();
            set => Set(value);
        }

        public bool IsAreaSelected
        {
            get => Get<bool>();
            set => Set(value);
        }

        public string PrivateText
        {
            get => Get<string>();
            set
            {
                Set(value);

                if (!_isLoadingNote)
                    IsSaveEnabled = true;
            }
        }

        public string PublicText
        {
            get => Get<string>();
            set
            {
                Set(value);

                if (!_isLoadingNote)
                    IsSaveEnabled = true;
            }
        }

        public int SelectedAreaIndex
        {
            get => Get<int>();
            set => Set(value);
        }

        private void LoadNote()
        {
            if (SelectedAreaIndex <= -1)
                return;

            _isLoadingNote = true;

            var query = new DBQuery()
                .AddFieldSearch(nameof(AreaNote.AreaResref), AreaResrefs[SelectedAreaIndex], false)
                .OrderBy(nameof(AreaNote.AreaResref));
            var notes = DB.Search<AreaNote>(query)
                .ToList();

            foreach (var note in notes)
            {
                PrivateText = note.PrivateText;
                PublicText = note.PublicText;
                _isLoadingNote = false;
            }

            if (_isLoadingNote)
            {
                var dbNote = new AreaNote
                {
                    AreaResref = AreaResrefs[SelectedAreaIndex]
                };
                DB.Set(dbNote);
            }

            _isLoadingNote = false;
            IsSaveEnabled = false;
        }

        private void SaveNote()
        {
            if (SelectedAreaIndex <= -1)
                return;

            var query = new DBQuery()
                .AddFieldSearch(nameof(AreaNote.AreaResref), AreaResrefs[SelectedAreaIndex], false)
                .OrderBy(nameof(AreaNote.AreaResref));
            var notes = DB.Search<AreaNote>(query)
                .ToList();

            foreach (var note in notes)
            {
                note.PrivateText = PrivateText;
                note.PublicText = PublicText;
                _isLoadingNote = false;
            }

            var message = AreaNames[SelectedAreaIndex] + ": " + notes[0].PublicText;
            foreach (var player in AreaCache.GetPlayersInArea(_areas[SelectedAreaIndex]))
            {
                SendMessageToPC(player, ColorToken.Purple(message));
            }

            DB.Set(notes[0]);
            IsSaveEnabled = false;
        }

        public Action OnCloseWindow() => SaveNote;

        public Action OnClickDeleteNote() => () =>
        {
            if (SelectedAreaIndex < 0)
                return;

            ShowModal($"Are you sure you want to delete the note for this area? '{AreaNames[SelectedAreaIndex]}'", () =>
            {
                PrivateText = string.Empty;
                PublicText = string.Empty;
                _isLoadingNote = true;
                IsAreaSelected = false;
                IsDeleteEnabled = false;
                IsSaveEnabled = false;
                _isLoadingNote = false;

                SaveNote();
            });

            IsSaveEnabled = false;
        };

        public Action OnSelectNote() => () =>
        {
            if (SelectedAreaIndex > -1)
                AreaToggled[SelectedAreaIndex] = false;

            var index = NuiGetEventArrayIndex();
            SelectedAreaIndex = index;

            LoadNote();

            IsDeleteEnabled = true;
            AreaToggled[index] = true;
            IsAreaSelected = true;

            IsSaveEnabled = false;
        };

        public Action OnClickSave() => SaveNote;

        public Action OnClickDiscardChanges() => () =>
        {
            LoadNote();
            IsSaveEnabled = false;
        };

        private void Search()
        {
            var areaResrefs = new XMBindingList<string>();
            var areaNames = new XMBindingList<string>();
            var areaToggled = new XMBindingList<bool>();

            _areas.Clear();
            AreaToggled.Clear();
            AreaNames.Clear();
            AreaResrefs.Clear();

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                foreach (var area in AreaCache.GetAllAreas())
                {
                    _areas.Add(area.Value);
                    areaResrefs.Add(area.Key);
                    areaNames.Add(GetName(area.Value));
                    areaToggled.Add(false);
                }
            }
            else
            {
                foreach (var area in AreaCache.GetAllAreas())
                {
                    if (GetStringUpperCase(GetName(area.Value)).Contains(GetStringUpperCase(SearchText)))
                    {
                        _areas.Add(area.Value);
                        areaResrefs.Add(area.Key);
                        areaNames.Add(GetName(area.Value));
                        areaToggled.Add(false);
                    }
                }
            }

            SelectedAreaIndex = -1;
            AreaResrefs = areaResrefs;
            AreaNames = areaNames;
            AreaToggled = areaToggled;
            PrivateText = string.Empty;
            PublicText = string.Empty;
            IsAreaSelected = false;
            IsSaveEnabled = false;
        }

        public Action OnClickSearch() => Search;

        public Action OnClickClearSearch() => () =>
        {
            SearchText = string.Empty;
            Search();
        };

        public override void OnOpen()
        {
            var areaResrefs = new XMBindingList<string>();
            var areaNames = new XMBindingList<string>();
            var areaToggled = new XMBindingList<bool>();

            _areas.Clear();

            foreach (var area in AreaCache.GetAllAreas())
            {
                _areas.Add(area.Value);
                areaResrefs.Add(area.Key);
                areaNames.Add(GetName(area.Value));
                areaToggled.Add(false);
            }

            SelectedAreaIndex = -1;
            AreaResrefs = areaResrefs;
            AreaNames = areaNames;
            AreaToggled = areaToggled;
            PrivateText = string.Empty;
            PublicText = string.Empty;
            IsAreaSelected = false;
            IsSaveEnabled = false;

            SearchText = string.Empty;
            Search();

            WatchOnClient(model => model.PrivateText);
            WatchOnClient(model => model.PublicText);
            WatchOnClient(model => model.SearchText);
        }

        public override void OnClose()
        {

        }
    }
}
