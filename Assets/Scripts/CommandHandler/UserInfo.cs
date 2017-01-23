using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Rights
{
	public Dictionary<e_rights, string> rights;

	public enum e_rights
	{
		guest = 1,
		invited = 2,
		admin = 3
	}

	public Rights()
	{
		rights = new Dictionary<e_rights, string>();
		rights.Add(e_rights.guest, "guest");
		rights.Add(e_rights.invited, "invited");
		rights.Add(e_rights.admin, "admin");
	}

	public bool rightExists(string right)
	{
		for (int i = 0; i < rights.Count; ++i)
			if (rights[(e_rights)i] == right)
				return true;
		return false;
	}

	public string listRights()
	{
		string result = "";

		for (int i = 1; i <= rights.Count; ++i)
			result += rights[(e_rights)i]  + "\n";
		return result;
	}
}

public class UserInfo
{
	public Rights r;
	public string pseudo { get; set; }
	public string rights { get; set; }

	public UserInfo()
	{
		r = new Rights();
		pseudo = "Bakunine";
		rights = r.rights[Rights.e_rights.guest];
	}

	public void changeRights(string right)
	{
		rights = right;
	}
}
