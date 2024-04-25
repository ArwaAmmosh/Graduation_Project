using Graduation_Project.Infrastructure.Abstract;

namespace Graduation_Project.Infrastructure.Repository
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        private DbSet<UserRefreshToken> userRefreshToken;
        #endregion

        #region Constructors
        public RefreshTokenRepository(UNITOOLDbContext dbContext) : base(dbContext)
        {
            userRefreshToken = dbContext.Set<UserRefreshToken>();
        }
        #endregion

        #region Handle Functions
        #endregion

    }
}
