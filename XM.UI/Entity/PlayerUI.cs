using Anvil.Services;
using System.Collections.Generic;
using XM.Data;
using XM.UI.Component;
using XM.UI.WindowDefinition;

namespace XM.UI.Entity
{
    [ServiceBinding(typeof(IDBEntity))]
    internal class PlayerUI: EntityBase
    {
        public PlayerUI()
        {
            WindowGeometries = new Dictionary<GuiWindowType, GuiRectangle>();

        }

        public PlayerUI(string playerId)
        {
            Id = playerId;
            WindowGeometries = new Dictionary<GuiWindowType, GuiRectangle>();
        }

        public Dictionary<GuiWindowType, GuiRectangle> WindowGeometries { get; set; }
    }
}
