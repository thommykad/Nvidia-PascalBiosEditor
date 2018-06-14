using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace PascalTDPTweaker.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("PascalBiosEditor.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		internal static Bitmap icon_21
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("icon_21", Resources.resourceCulture);
			}
		}

		internal static Bitmap logo
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("logo", Resources.resourceCulture);
			}
		}

		internal static Bitmap logo_1
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("logo_1", Resources.resourceCulture);
			}
		}

		internal static Bitmap logo_21
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("logo_21", Resources.resourceCulture);
			}
		}

		internal static Bitmap logo_80
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("logo_80", Resources.resourceCulture);
			}
		}

		internal static Bitmap logo_85
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("logo_85", Resources.resourceCulture);
			}
		}

		internal static Bitmap logo_88
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("logo_88", Resources.resourceCulture);
			}
		}

		internal Resources()
		{
		}
	}
}
