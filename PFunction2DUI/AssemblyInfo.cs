using Mono.Addins;
using System.Reflection;
using System.Runtime.InteropServices;

// Nei progetti di tipo SDK come questo diversi attributi di assembly che sono stati
// definiti cronologicamente in questo file vengono ora aggiunti automaticamente durante
// la compilazione e popolati con i valori definiti nelle proprietà del progetto.
// Per informazioni dettagliate sugli attributi inclusi e su come personalizzare questo
// processo, vedere: https://aka.ms/assembly-info-properties


// The following lines tell that the assembly is an addin.
[assembly: Addin("Point Function 2D UI", "1.0.5", Category = "Modeling Tools")]
[assembly: AddinDependency("Fairmat", "1.0")]
[assembly: AddinAuthor("Fairmat SRL")]
[assembly: AddinDescription("A 2D function defined by interpolating or fitting data.")]

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Point Function 2D UI")]
[assembly: AssemblyDescription("A 2D function defined by interpolating or fitting data.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Fairmat SRL")]
[assembly: AssemblyProduct("Point Function 2D UI")]
[assembly: AssemblyCopyright("Copyright © Fairmat SRL 2012")]
[assembly: AssemblyTrademark("Fairmat")]
[assembly: AssemblyCulture("")]


// Se si imposta ComVisible su false, i tipi in questo assembly non saranno visibili
// ai componenti COM. Se è necessario accedere a un tipo in questo assembly da COM,
// impostare su true l'attributo ComVisible per tale tipo.

[assembly: ComVisible(false)]

// Se il progetto viene esposto a COM, il GUID seguente verrà usato come ID di typelib.

[assembly: Guid("e5b49f7d-6855-430e-9762-01f327cc1b90")]
