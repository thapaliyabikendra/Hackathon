namespace Hackathon.Permissions;

public static class HackathonPermissions
{
    public const string GroupName = "Hackathon";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
	public static class Teams	{
		public const string Default = GroupName + ".Teams";
		public const string Create = Default + ".Create";
		public const string Edit = Default + ".Edit";
		public const string Delete = Default + ".Delete";
	}
	public static class Matchs	{
		public const string Default = GroupName + ".Matchs";
		public const string Create = Default + ".Create";
		public const string Edit = Default + ".Edit";
		public const string Delete = Default + ".Delete";
	}
	public static class Groups	{
		public const string Default = GroupName + ".Groups";
		public const string Create = Default + ".Create";
		public const string Edit = Default + ".Edit";
		public const string Delete = Default + ".Delete";
	}
	public static class Stadiums	{
		public const string Default = GroupName + ".Stadiums";
		public const string Create = Default + ".Create";
		public const string Edit = Default + ".Edit";
		public const string Delete = Default + ".Delete";
	}
	public static class Tournaments	{
		public const string Default = GroupName + ".Tournaments";
		public const string Create = Default + ".Create";
		public const string Edit = Default + ".Edit";
		public const string Delete = Default + ".Delete";
	}
}
