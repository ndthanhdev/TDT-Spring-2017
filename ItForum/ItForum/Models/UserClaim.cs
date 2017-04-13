using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace ItForum.Models
{
    // Source from https://github.com/aspnet/Identity/blob/dev/src/Microsoft.AspNetCore.Identity.EntityFrameworkCore/IdentityUserClaim.cs
    public class UserClaim
    {
        /// <summary>
        ///     Gets or sets the identifier for this user claim.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual string Id { get; set; }

        /// <summary>
        ///     Gets or sets the primary key of the user associated with this claim.
        /// </summary>
        public virtual string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        /// <summary>
        ///     Gets or sets the claim type for this claim.
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        ///     Gets or sets the claim value for this claim.
        /// </summary>
        public virtual string ClaimValue { get; set; }

        /// <summary>
        ///     Converts the entity into a Claim instance.
        /// </summary>
        /// <returns></returns>
        public virtual Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }

        /// <summary>
        ///     Reads the type and value from the Claim.
        /// </summary>
        /// <param name="claim"></param>
        public virtual void InitializeFromClaim(Claim claim)
        {
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
    }
}