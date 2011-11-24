//
//! Copyright © 2008-2011
//! Brandon Kohn
//
//  Distributed under the Boost Software License, Version 1.0. (See
//  accompanying file LICENSE_1_0.txt or copy at
//  http://www.boost.org/LICENSE_1_0.txt)
//
using System;
using System.Windows.Forms.Design;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.Win32;
using IOleServiceProvider = Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
using Microsoft.VisualStudio;

namespace Meta
{
    public static class ProjectHelper
    {
        internal static bool IsCPPProject(uint projectItemId, IVsHierarchy node)
        {
            object value;
            node.GetProperty(projectItemId, (int)__VSHPROPID.VSHPROPID_TypeName, out value);
            return (value != null && value.ToString() == "Microsoft Visual C++");
        }

        internal static bool IsCPPNode(uint projectItemId, IVsHierarchy node)
        {
            object value;
            node.GetProperty(projectItemId, (int)__VSHPROPID.VSHPROPID_Name, out value);

            if (value == null)
                return false;

            string value_str = value.ToString().ToLowerInvariant();
            return (value_str.EndsWith(".cpp") ||
                    value_str.EndsWith(".cxx") ||
                    value_str.EndsWith(".cc") ||
                    value_str.EndsWith(".c"));
        }

        internal static EnvDTE.Project GetProject(IVsHierarchy hierarchy)
        {
            object project;
            ErrorHandler.ThrowOnFailure(hierarchy.GetProperty(VSConstants.VSITEMID_ROOT, (int)__VSHPROPID.VSHPROPID_ExtObject, out project));
            return (project as EnvDTE.Project);
        }
    }
}