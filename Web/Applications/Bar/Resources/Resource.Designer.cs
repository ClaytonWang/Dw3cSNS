﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Spacebuilder.Bar.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Spacebuilder.Bar.Resources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 帖吧.
        /// </summary>
        internal static string ApplicationName_Bar {
            get {
                return ResourceManager.GetString("ApplicationName_Bar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 讨论.
        /// </summary>
        internal static string ApplicationName_GroupSpace_Bar {
            get {
                return ResourceManager.GetString("ApplicationName_GroupSpace_Bar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 您现在处于管制状态，不能回帖.
        /// </summary>
        internal static string Description_ModeratedUser_CreateBarPostDenied {
            get {
                return ResourceManager.GetString("Description_ModeratedUser_CreateBarPostDenied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 您现在处于管制状态，不能发帖.
        /// </summary>
        internal static string Description_ModeratedUser_CreateBarThreadDenied {
            get {
                return ResourceManager.GetString("Description_ModeratedUser_CreateBarThreadDenied", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 回复：.
        /// </summary>
        internal static string Post_Subject_Prefix {
            get {
                return ResourceManager.GetString("Post_Subject_Prefix", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 您最多输入{1}个字.
        /// </summary>
        internal static string Validate_TextTooLong {
            get {
                return ResourceManager.GetString("Validate_TextTooLong", resourceCulture);
            }
        }
    }
}