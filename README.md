[![Stories in Ready](https://badge.waffle.io/lou1306/CIV.png?label=ready&title=Ready)](https://waffle.io/lou1306/CIV?utm_source=badge)
[![Build Status](https://travis-ci.org/lou1306/CIV.svg?branch=master)](https://travis-ci.org/lou1306/CIV)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

# CIV - CCS Interpreter and Verifier

CIV (pronounced *One-Zero-Four* or *CentoQuattro*) is a tool to verify properties on a CCS process.
It includes Antlr4-based parsers for CCS and (hopefully) HML with fixpoints. 

I do *not* want to build a super-efficient or "enterprise-ready" implementation.
This will be, first and foremost, an educational project to show off the capabilities of Antlr4 and C# on the .NET Core platform. 
And to get to know CCS/HML a little deeper :)

## Requirements

You need java if you want to re-generate the lexers/parsers during the build phase (in Debug mode).

You must have installed .NET Core 2.0 for this software to run. You can verify your version by running 
```
dotnet --info
```
in a terminal.

## Building and running

```
cd <your-favorite-dir>
git clone https://github.com/lou1306/CIV.git
cd CIV/CIV
dotnet restore
dotnet run -c Release
```

## Remove Antlr build step

If you do not want to mess with the files, just run CIV in release mode:
```
dotnet run -c Release
```
To avoid the Antlr step in debug mode, open `CIV/CIV.csproj` and delete or comment out everything between `<CustomCommands>` and `</CustomCommands>`.


## Influences

This project has been greatly influenced by [CAAL](http://caal.cs.aau.dk/) and aims at maximum compatibility with its `.caal`format.
[TAPAS](http://etapas.sourceforge.net/) is also a source of inspiration: basically I'm trying to merge CAAL's ease of use with TAPAS' performance and general non-"itrunsinthebrowser"-ness.

Some *very* useful Antlr tutorials are available
[here](https://tomassetti.me/antlr-mega-tutorial/ )
and
[here](https://tomassetti.me/getting-started-with-antlr-in-csharp/).
## Disclaimer

Use at your own risk. However, if you're messing with CCS you're already accustomed to laughing in the face of danger.
