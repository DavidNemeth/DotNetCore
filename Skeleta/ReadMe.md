Skeleton application to serve as a starting point for building well structured, reusable and robust multilayer enterprise solutions.  
  
  
TODO:  
**-Enable offline db changes with sync later option**    
**-Builtin chat and notification system**     
**-Auth services**       


ChangeSets:   
  
 **2018:**   
 **-Added modal edit and add for sample datasets**    
 **-Sample crud working with good speed (large dataset added for testing)**   
 **-Menu and sidebar changed for simplicity** 
  
**2017.11.16:**  
**-Implemented Auto reconnect**  
**-Started Offline work mode**    
  
**2017.11.09:**  
**-Added productForm**  
**-Sample api calls with Observables**  
  
**2017.11.06:**  
**-Added product-detail page**  
**-Configured angular routing**  
  
**2017.11.03:**  
**-Packages updated**  
**-Added angular get for sample list**  
**-Added viewmodel and mapping**  
  
**2017.11.02:**    
**-Updated Repository and UoW to Async methods**    
**-Added Rest Controller for products**    
**-Added products service to angular**  
**-Added Base Controller**  

**2017.10.31:**  
**-Updated db with products**  
**-Sample MVC views and angular view**  
**-Test APIs and Controllers**

**2017.10.20:**  
**-Model Update**  
**-Added Sample API, paging, and sample services**  

**2017.10.19:**  
**-Setup Db creation and seeding**  
**-Configured Email sender, and implemented helper class**  
**-Added Swagger for api documentation**  
**-Registered OpenId services with OpenIddict**  
**-Configured Authentication with OAuth**  
**-Configured Identity**  
**-Seeded database, fixed setup config**  
**-Added ViewModels and AutoMapper**  

**2017.10.18:**  
**-DataAccesLayer with UnitOfWork and Repository pattern implemented.**  
**-User and premission based role management on backend implemented.(Able to create and manage roles, premissions etc so it is configurable for any business requirement)**  
  
  
  
  
  
Technologies will include:   

Backend:    
            
Generic repository DataAccessLayer exposing IUnitOfWork interface.    
Inheritable base model to track changes.    
Hybrid SPA Applicaion making use of .net core MVC controllers Together with Restfull APIs to create responsive, modern, yet safe application layer.   
Swagger for API documentation.    
Automapper to "copy" data from entities to DTOs. Easy to use, fast and extensible.    
        
        
             
FrontEnd:   
            
Razor views to serve MVC controllers, modern javascript framework for SPA parts.(Angular 4).    
Sass (css extension):  Its key features are the ability to use variables, nesting, mixins and loops within css.   
Bootstrap: Reusable, robust and attractive styles for everyday styling.   
Bower: Front-end package management tool.



