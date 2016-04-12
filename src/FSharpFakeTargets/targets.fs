namespace DatNET

module Targets =
  open Fake
  open Fake.FileSystem
  open System.IO

  let private RootDir = Directory.GetCurrentDirectory()

  type ConfigParams =
    {
      SolutionFile : FileIncludes
      MSBuildArtifacts : FileIncludes
    }

  let ConfigDefaults() =
    {
      SolutionFile = !! (Path.Combine(RootDir, "*.sln"))
      MSBuildArtifacts = !! "**/bin/**.*" ++ "**/obj/**.*"
    }

  let private _CreateTarget targetName parameters targetFunc =
    Target targetName targetFunc
    parameters

  let private _MSBuildTarget parameters =
    _CreateTarget "MSBuild" parameters (fun _ ->
        parameters.SolutionFile
            |> MSBuildRelease null "Build"
            |> ignore
    )

  let private _CleanTarget parameters =
    _CreateTarget "Clean" parameters (fun _ ->
         CleanDirs parameters.MSBuildArtifacts
    )

  let Initialize setParams =
    let parameters = ConfigDefaults() |> setParams

    parameters
        |> _MSBuildTarget
        |> _CleanTarget
