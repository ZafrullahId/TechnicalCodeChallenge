# Technical Code Challenge Solutions

This repository contains the solutions for two coding challenges:

---

## Solution 1: FileScanner

### Description
The `FileScanner` class scans files in a local folder, searching for a specified string. It outputs whether the string is present or absent in each file.

### Features:
- Handles large file sizes efficiently.
- Case-insensitive search for the specified string.

### Usage

```csharp
var fileScanner = new FileScanner("path_to_folder", "search_string");
fileScanner.ScanFiles();
```

### Assumptions
- Assumed that files are locally available.
- Large file handling is addressed by reading files sequentially to avoid memory overload.

## Solution 2: DuplicateFinder

### Description
The `DuplicateFinder` class checks if elements in collection C(S) are present in collection C(A) and outputs the result.

### Features:
- Uses IEquatable<T> to compare elements.
- Outputs each element with a true or false based on its presence in C(A).

### Usage

```csharp
var finder = new DuplicateFinder<int>(collectionA, collectionS);
finder.FindDuplicates();
```
#### Assumptions
- Both collections are of the same type implementing IEquatable<T>.

## Requirements
- .NET Core 9 SDK
- xUnit for testing
