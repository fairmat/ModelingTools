using Mono.Addins;
using System.Runtime.InteropServices;

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("d8cf708f-3c72-4b79-8f91-66f09524382e")]

[assembly: Addin("Point Function 2D", "1.0.5", Category = "Modeling Tools")]
[assembly: AddinDependency("Fairmat", "1.0")]
[assembly: AddinAuthor("Fairmat SRL")]
[assembly: AddinDescription("A 2D function defined by interpolating or fitting data.")]
