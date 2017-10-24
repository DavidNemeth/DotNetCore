Skeleton application to serve as a starting point for building well structured, reusable and robust multilayer enterprise solutions.

ChangeSets:


**2017.10.18:**  
**-DataAccesLayer with UnitOfWork and Repository pattern implemented.**  
**-User and premission based role management on backend implemented.(Able to create and manage roles, premissions etc so you can make it fit with different business requirements)**  
  
**2017.10.19:**  
**-Setup Db creation and seeding**  
**-Configured Email sender, and implemented helper class**  
**-Added Swagger for api documentation**  
**-Registered OpenId services with OpenIddict**  
**-Configured Authentication with OAuth**  
**-Configured Identity**  
**-Seeded database, fixed setup config**  
**-Added ViewModels and AutoMapper**  
**2017.10.20:**  
**-Model Update**  
**-Added Sample API, paging, and sample services**


  
  
  
  
  
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



