Architecture
================

1. Projects deploying to modern platforms will target the LTS version (highest even number) of .NET.
2. For legacy Windows deployments:
	- Windows 98 => .NET Framework 2.0
	- Windows XP => .NET Framework 3.5
	- Windows 7  => .NET Framework 4.5.1
3. Follow a [Three-Tier Architecture](https://www.ibm.com/topics/three-tier-architecture) (Data, Business, Presentation). Logical boundaries can be further separated as needed.
	- These projects target desktop and are intended to represent small utilities, so closer to layers as they deploy together. However;
		- Despite one layer directly referencing the one below, only protocols, services, and DTOs intentionally made to be exposed to other layers should be consumed.
		- Enforce separation of concerns. 
			- Business doesn't define entities. 
			- Data access doesn't implement business validations.
			- UI doesn't perform business logic.
		- Maintain modularity and [Locality of Behavior](https://htmx.org/essays/locality-of-behaviour/) within the application/business layer.
4. Name projects with the postfix of their tier (e.g., MyCoolApp.Data).
5. There can be more than one presentation for the set of given business functionality (i.e., a mobile app and a desktop application). Name each presentation project with the target platform postfix (e.g., MyCoolApp.Presentation.Web).
6. GUI app projects that target desktop or mobile and support binding should follow the [MVVM Design Pattern](https://dev.to/mochafreddo/mastering-mvvm-a-comprehensive-guide-to-the-model-view-viewmodel-architecture-221g).
7. CLI app projects should follow a Model–View–Presenter design pattern.
8. Design pattern naming conventions:
     * View class names always end with the word "View".
     * Presenter class names always end with the Word "Presenter".
     * ViewModel class names always end with the Word "ViewModel".
     * Controller class names always end with the word "Controller".
     * Names of other kinds of classes <ins>never</ins> end in these words.


Business Classes
================

Generally follow the definitions described within this [article](https://medium.com/@mishraabhinn/understanding-domain-objects-entities-dtos-and-models-in-c-207bb5c1d97c).

1. **Entities** - Uniquely identifiable objects. This includes everything that would get mapped to a database table.
2. **DTOs** - Lightweight objects for data transfer. All should be considered serializable and made as records. Always a [POCO](https://ardalis.com/dto-or-poco/).
3. **Domains** - Real-world concepts used internally within business logic and don't need to persist.
4. **Models** - Can represent a view of either entities, domains, or DTOs. With the intention that these are what would be consumed by the presentation layer. In MVVM and MVC, models are the objects used for data binding.

Ideally application DTOs get validated, entities are pure data objects, and in general objects (outside of some effectively static services) don't get complex behavior. [KISS](https://www.interaction-design.org/literature/topics/keep-it-simple-stupid).

Coding
================

1. Prefer file scoped namespaces, to reduce indenting.
2. Prefer `Foo fooInstance = new();` over `var fooInstance = new Foo();`.
3. Name constants in all caps and snake case (underscores between words) (e.g., `const string API_ENDPOINT = "https://rest.example.com/v1";`).
4. Name private fields as camel case with a leading undercore (e.g., `private int _counter = 0;`).
5. Name public and internal members as pascal case (e.g., `public required DateTime TimeStamp { get; set; }`).
6. Generally follow [this](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) for C#.
7. Generally follow [this](https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/) for libraries.
8. Generally follow [this](https://www.infoq.com/articles/Exceptions-API-Design/) for exceptions.
