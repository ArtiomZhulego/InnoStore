using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class InnoStoreContext(DbContextOptions options) : DbContext(options)
{
}
