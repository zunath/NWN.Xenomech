using Anvil.Services;
using XM.Shared.API.NWNX.AdminPlugin;
using XM.Shared.Core.Data;
using XM.Shared.Core.Entity;

namespace XM.Plugin.Administration.BanManagement
{
    [ServiceBinding(typeof(BanService))]
    internal class BanService: IInitializable
    {
        private readonly DBService _db;

        public BanService(DBService db)
        {
            _db = db;
        }

        public void Init()
        {
            ApplyBans();
        }

        private void ApplyBans()
        {
            var query = new DBQuery();

            var dbBanCount = _db.SearchCount<PlayerBan>(query);
            var dbBans = _db.Search<PlayerBan>(query.AddPaging(dbBanCount, 0));

            foreach (var ban in dbBans)
            {
                AdminPlugin.AddBannedCDKey(ban.CDKey);
            }
        }
    }
}
