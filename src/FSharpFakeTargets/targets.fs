namespace DatNET

module Targets =
  open Fake
  open Fake.FileSystem
  open System.IO

  let private RootDir = Directory.GetCurrentDirectory()

  type ConfigParams =
    {
      SolutionFile : FileIncludes
    }

  let ConfigDefaults() =
    {
      SolutionFile = !! (Path.Combine(RootDir, "*.sln"))
    }