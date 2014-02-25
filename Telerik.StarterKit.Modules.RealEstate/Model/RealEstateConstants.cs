using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.StarterKit.Modules.RealEstate.Model
{
    /// <summary>
    /// Various constants related to Real Estate module
    /// </summary>
    public static class RealEstateConstants
    {
        /// <summary>
        /// Security-Related constants related to Items
        /// </summary>
        public static class Security
        {
            /// <summary>
            /// The main permission set name related to Real Estate module
            /// </summary>
            public const string PermissionSetName = "RealEstate";

            /// <summary>
            /// View action name
            /// </summary>
            public const string View = "ViewItems";

            /// <summary>
            /// Modify Items action name
            /// </summary>
            public const string Modify = "ModifyItems";

            /// <summary>
            /// Create Items action name
            /// </summary>
            public const string Create = "CreateItems";

            /// <summary>
            /// Delete Items action name
            /// </summary>
            public const string Delete = "DeleteItems";

            /// <summary>
            /// ChangeOwner Items action name
            /// </summary>
            public const string ChangeOwner = "ChangeItemsOwner";

            /// <summary>
            /// ChangePermissions Items action name
            /// </summary>
            public const string ChangePermissions = "ChangeItemsPermissions";
        }
    }
}
