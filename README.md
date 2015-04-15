# Lanem
**L**ight **A**sp.**N**et **E**rror Handler **M**odule for processing and logging ASP.NET application exceptions.

## About
This is an extremely light weight HttpModule to gobally handle errors in an ASP.NET web application.

It purposely doesn't implement any HTTP endpoints to consume error logs from a remote location.

### Why not having a feature is a feature
In many enterprise applications it is not desired and even consiered as harmful to expose error logs via a web accesible API.

I have seen too many websites using ELMAH and not having their logs secured at all or not properly. After all security is difficult to get right and opening a door by default is an anti pattern.

### Secure your logs properly
Error logs are highly sensitive pieces of information. The extent goes often far beyond the exposure of your internal application architecture.

They often contain personal user information and depending on the context of a website this PI might be considered as highly confidential data.

Therefore, good practise is to store error logs in a secure location outside the wwwroot folder. Only a handful of (dev)ops might have access to the logs via FTP or other well established channels which provide good security out of the box.

## Implementation

### Out of the box
By default the project installs a very simple ErrorHandlerModule. This module logs all unhandled exceptions in a human friendly readable format on disk.

The path to the error log directory can be configured in the Web.config.

### Customisation
You can replace the entire error handling by overriding the OnError method or you can change individual parts by implementing one of the default interfaces.

Currently you can change the exception formatting, error filtering and error logging.

Simply provide a new implementation of the according interface and plug it into the ErrorHandlerModule.

## Installation

Please use the official NuGet feed to download and install this package in your application.

This project follows the [semver](http://semver.org/) versioning conventions.

## Contribution

Feedback, critics and pull requests are all more than welcome!

You can either contact me via my GitHub profile or find me on [http://dusted.codes](http://dusted.codes).