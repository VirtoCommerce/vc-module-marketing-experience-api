using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using VirtoCommerce.Platform.Core;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Security;

namespace VirtoCommerce.MarketingExperienceApi.Data.Authorization;

public class PromotionCouponsAuthorizationRequirement : IAuthorizationRequirement
{
}

public class PromotionCouponsAuthorizationHandler : AuthorizationHandler<PromotionCouponsAuthorizationRequirement>
{
    private readonly Func<UserManager<ApplicationUser>> _userManagerFactory;

    public PromotionCouponsAuthorizationHandler(Func<UserManager<ApplicationUser>> userManagerFactory)
    {
        _userManagerFactory = userManagerFactory;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PromotionCouponsAuthorizationRequirement requirement)
    {
        var result = context.User.IsInRole(PlatformConstants.Security.SystemRoles.Administrator);

        if (!result && context.Resource is string storeId)
        {
            var currentUserId = context.User.GetUserId();
            using var userManager = _userManagerFactory();
            var user = await userManager.FindByIdAsync(currentUserId);

            if (user?.StoreId != null)
            {
                result = user.StoreId.EqualsIgnoreCase(storeId);
            }
        }

        if (result)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
