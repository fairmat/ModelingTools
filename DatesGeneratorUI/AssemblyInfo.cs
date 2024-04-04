using Mono.Addins;
using System.Reflection;
using System.Runtime.InteropServices;

// Nei progetti di tipo SDK come questo diversi attributi di assembly che sono stati
// definiti cronologicamente in questo file vengono ora aggiunti automaticamente durante
// la compilazione e popolati con i valori definiti nelle proprietà del progetto.
// Per informazioni dettagliate sugli attributi inclusi e su come personalizzare questo
// processo, vedere: https://aka.ms/assembly-info-properties


// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Dates Generator UI")]
[assembly: AssemblyDescription("Dates generator allows to generate sequences of (payment) dates " +
                               "by specifying Starting Date, Ending Date and the frequency.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Fairmat SRL")]
[assembly: AssemblyProduct("Dates Generator UI")]
[assembly: AssemblyCopyright("Copyright © Fairmat SRL 2012-2017")]
[assembly: AssemblyTrademark("Fairmat")]
[assembly: AssemblyCulture("")]



// Se si imposta ComVisible su false, i tipi in questo assembly non saranno visibili
// ai componenti COM. Se è necessario accedere a un tipo in questo assembly da COM,
// impostare su true l'attributo ComVisible per tale tipo.

[assembly: ComVisible(false)]

// Se il progetto viene esposto a COM, il GUID seguente verrà usato come ID di typelib.

[assembly: Guid("8223a8ab-fa22-4f7b-93dc-7b25a69432d4")]

[assembly: AssemblyVersion("1.0.29")]
[assembly: AssemblyFileVersion("1.0.29")]

[assembly: Addin("Dates Generator UI", "1.0.29", Category = "Modeling Tools")]
[assembly: AddinDependency("Fairmat", "1.0")]
[assembly: AddinAuthor("Fairmat SRL")]
