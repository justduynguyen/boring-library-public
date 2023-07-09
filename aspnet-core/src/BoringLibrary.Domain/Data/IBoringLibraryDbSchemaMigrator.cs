using System.Threading.Tasks;

namespace BoringLibrary.Data;

public interface IBoringLibraryDbSchemaMigrator
{
    Task MigrateAsync();
}