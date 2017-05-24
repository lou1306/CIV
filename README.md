# CIV - CCS Interpreter and Verifier

CIV (pronounced *One-Zero-Four* or *CentoQuattro*) is a tool to verify properties on a CCS process.
It includes ANTLR-based parsers for CCS and (hopefully) HML with fixpoints. 

## Requirements

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
dotnet run
```

## Influences

This project has been greatly influenced by [CAAL](http://caal.cs.aau.dk/) and aims at maximum compatibility with its `.caal`format.
[TAPAS](http://etapas.sourceforge.net/) is also a source of inspiration: basically I'm trying to merge CAAL's ease of use with TAPAS' performance and general non-"itrunsinthebrowser"-ness.

## Disclaimer

Use at your own risk. However, if you're messing with CCS you're already accustomed to laughing in the face of danger.
