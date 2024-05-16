using Mono.Addins;

// The following lines tell that the assembly is an addin.
[assembly: Addin("Point Function 2D", "1.0.5", Category = "Modeling Tools")]
[assembly: AddinDependency("Fairmat", "1.0")]
[assembly: AddinAuthor("Fairmat SRL")]
[assembly: AddinDescription("A 2D function defined by interpolating or fitting data.")]