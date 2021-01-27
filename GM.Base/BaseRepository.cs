using GeoMed.SqlServer;

namespace GM.Base
{
    public class BaseRepository
    {
        protected readonly GMApiContext Context;
        protected BaseRepository(GMApiContext context)
        {
            Context = context;
        }
    }
}
