using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UpdateGroupMasterModel
/// </summary>
public class UpdateGroupMasterModel
{
    public int Ind { get; set; }
    public int OrgID { get; set; }
    public int BrID { get; set; }
    public int GrType { get; set; }
    public string GrDesc { get; set; }
    public int ParentID { get; set; }
    public int ItemGroupID { get; set; }
    public int YrCD { get; set; }
    public int User { get; set; }
    public string IP { get; set; }
}