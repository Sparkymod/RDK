using Dagher.ORM.SubSonic.DataProviders;
using Dagher.ORM.SubSonic.Schema;

namespace Dagher.ORM
{
    public interface IManualGeneratedRecord
    {
        ITable GetTableInformation(IDataProvider provider); 
    }
}