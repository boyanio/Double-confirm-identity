# Double confirm identity
An additional steps in ASP.NET Identity that allows logged in users to confirm their identity one more time.

# Use case scenario
A user logs in using any popular method, f.x., forms authentication, which results in a cookie being set. This already allows the user to access some parts of the web site. However, you may want to protect other parts even more. In this case, the user will be required to log in again using some other method, which is typically more secure. This can be OAuth, for example, from your server.

Of course users may choose to log in directly the more secure way and will automatically get access to all resources on your web site.

# How it works
ASP.NET Identity relies on claims. When the user logs in second time, a special claim is added to his identity. You can check the [code](https://github.com/boyanin/Double-confirm-identity/tree/master/Examples/FormsAuth) of example project.
