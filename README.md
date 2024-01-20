# DoubleDoubleRootFinding
 DoubleDouble Root Finding Method Implements

## Requirement
.NET 8.0  
[DoubleDouble](https://github.com/tk-yoshimura/DoubleDouble)

## Install

[Download DLL](https://github.com/tk-yoshimura/DoubleDoubleRootFinding/releases)  
[Download Nuget](https://www.nuget.org/packages/tyoshimura.doubledouble.rootfinding/)  

## Usage
```csharp
// Newton-Raphson Method: solve x for x^3 = 2
static (ddouble v, ddouble d) f(ddouble x) {
    return (x * x * x - 2, 3 * x * x);
}

NewtonRaphsonFinder.RootFind(f, x0: 2);
```

## Licence
[MIT](https://github.com/tk-yoshimura/DoubleDoubleRootFinding/blob/main/LICENSE)

## Author

[T.Yoshimura](https://github.com/tk-yoshimura)
