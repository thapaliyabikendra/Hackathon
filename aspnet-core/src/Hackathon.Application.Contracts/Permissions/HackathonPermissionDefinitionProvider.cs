using Hackathon.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Hackathon.Permissions;

public class HackathonPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var hackathonGroup = context.AddGroup(HackathonPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HackathonPermissions.MyPermission1, L("Permission:MyPermission1"));
		var teamsPermission = hackathonGroup.AddPermission(HackathonPermissions.Teams.Default, L("Permission:Teams"));
		teamsPermission.AddChild(HackathonPermissions.Teams.Create, L("Permission:Teams.Create"));
		teamsPermission.AddChild(HackathonPermissions.Teams.Edit, L("Permission:Teams.Edit"));
		teamsPermission.AddChild(HackathonPermissions.Teams.Delete, L("Permission:Teams.Delete"));
		var matchsPermission = hackathonGroup.AddPermission(HackathonPermissions.Matchs.Default, L("Permission:Matchs"));
		matchsPermission.AddChild(HackathonPermissions.Matchs.Create, L("Permission:Matchs.Create"));
		matchsPermission.AddChild(HackathonPermissions.Matchs.Edit, L("Permission:Matchs.Edit"));
		matchsPermission.AddChild(HackathonPermissions.Matchs.Delete, L("Permission:Matchs.Delete"));
		var groupsPermission = hackathonGroup.AddPermission(HackathonPermissions.Groups.Default, L("Permission:Groups"));
		groupsPermission.AddChild(HackathonPermissions.Groups.Create, L("Permission:Groups.Create"));
		groupsPermission.AddChild(HackathonPermissions.Groups.Edit, L("Permission:Groups.Edit"));
		groupsPermission.AddChild(HackathonPermissions.Groups.Delete, L("Permission:Groups.Delete"));
		var stadiumsPermission = hackathonGroup.AddPermission(HackathonPermissions.Stadiums.Default, L("Permission:Stadiums"));
		stadiumsPermission.AddChild(HackathonPermissions.Stadiums.Create, L("Permission:Stadiums.Create"));
		stadiumsPermission.AddChild(HackathonPermissions.Stadiums.Edit, L("Permission:Stadiums.Edit"));
		stadiumsPermission.AddChild(HackathonPermissions.Stadiums.Delete, L("Permission:Stadiums.Delete"));
		var tournamentsPermission = hackathonGroup.AddPermission(HackathonPermissions.Tournaments.Default, L("Permission:Tournaments"));
		tournamentsPermission.AddChild(HackathonPermissions.Tournaments.Create, L("Permission:Tournaments.Create"));
		tournamentsPermission.AddChild(HackathonPermissions.Tournaments.Edit, L("Permission:Tournaments.Edit"));
		tournamentsPermission.AddChild(HackathonPermissions.Tournaments.Delete, L("Permission:Tournaments.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HackathonResource>(name);
    }
}
