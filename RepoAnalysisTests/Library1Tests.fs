namespace RepoAnalysisTests

open NUnit.Framework
open FsUnit
open RepoAnalysis

//type LightBulb(state) =
//    member x.On = state
//    override x.ToString() =
//        match x.On with
//        | true  -> "On"
//        | false -> "Off"

[<TestFixture>] 
type ``Create a class instance of MyFirst Class`` ()=
    let lightBulb = new MyFirstClass()

    [<Test>] member x.
     ``Test member value.`` ()=
            lightBulb.X |> should equal "Hello world"
