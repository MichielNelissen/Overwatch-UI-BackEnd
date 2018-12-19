using Microsoft.EntityFrameworkCore;
using OverwatchAPI.Data.Context;

namespace OverwatchAPI.Test.Builders
{
    public static class OverwatchOptionBuilder
    {
        public static DbContextOptions<OverwatchContext> CreateBuilderWithName(string dbName)
        {
            return new DbContextOptionsBuilder<OverwatchContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }
    }
}