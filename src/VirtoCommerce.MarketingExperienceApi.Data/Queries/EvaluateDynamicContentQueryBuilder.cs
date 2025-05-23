using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using VirtoCommerce.CustomerModule.Core.Model;
using VirtoCommerce.CustomerModule.Core.Services;
using VirtoCommerce.MarketingExperienceApi.Data.Schemas;
using VirtoCommerce.MarketingModule.Core.Model.DynamicContent;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Security;
using VirtoCommerce.Xapi.Core.BaseQueries;
using VirtoCommerce.Xapi.Core.Extensions;
using static VirtoCommerce.Xapi.Core.ModuleConstants;

namespace VirtoCommerce.MarketingExperienceApi.Data.Queries
{
    public class EvaluateDynamicContentQueryBuilder : QueryBuilder<EvaluateDynamicContentQuery, EvaluateDynamicContentResult, EvaluateDynamicContentResultType>
    {
        protected override string Name => "evaluateDynamicContent";

        private readonly Func<UserManager<ApplicationUser>> _userManagerFactory;
        private readonly IMemberService _memberService;

        public EvaluateDynamicContentQueryBuilder(
            IMediator mediator,
            IAuthorizationService authorizationService,
            Func<UserManager<ApplicationUser>> userManagerFactory,
            IMemberService memberService)
            : base(mediator, authorizationService)
        {
            _userManagerFactory = userManagerFactory;
            _memberService = memberService;
        }

        protected override async Task BeforeMediatorSend(IResolveFieldContext<object> context,
            EvaluateDynamicContentQuery request)
        {
            var userGroups = await GetUserGroups(context);
            request.UserGroups = request.UserGroups != null ? userGroups.Intersect(request.UserGroups).ToArray() : userGroups.ToArray();

            await base.BeforeMediatorSend(context, request);
        }


        private async Task<IList<string>> GetUserGroups(IResolveFieldContext<object> context)
        {
            var userId = GetUserId(context);
            var contact = await GetContact(userId);

            if (contact == null)
            {
                return Array.Empty<string>();
            }

            var userGroups = contact.Groups.ToList();

            var organizationId = context.GetCurrentOrganizationId();
            var organization = await GetOrganization(organizationId);
            if (organization?.Groups != null)
            {
                userGroups.AddDistinct(organization.Groups.ToArray());
            }

            return userGroups;
        }

        private static string GetUserId(IResolveFieldContext<object> context)
        {
            var user = context.GetCurrentPrincipal();
            return user.GetUserId() ?? AnonymousUser.UserName;
        }

        private async Task<Contact> GetContact(string userId)
        {
            Contact contact = null;

            var userManager = _userManagerFactory();
            var user = await userManager.FindByIdAsync(userId);

            if (!string.IsNullOrEmpty(user?.MemberId))
            {
                contact = await _memberService.GetByIdAsync(user.MemberId) as Contact;
            }

            return contact;
        }

        private async Task<Organization> GetOrganization(string organizationId)
        {
            Organization organization = null;

            if (!string.IsNullOrEmpty(organizationId))
            {
                organization = await _memberService.GetByIdAsync(organizationId) as Organization;
            }

            return organization;
        }
    }
}
