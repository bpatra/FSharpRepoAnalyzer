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
    let repo = new RepoManipulation.RepoManager(@"C:repo1")


    [<Test>] member x.
     ``First Commiter.`` ()=
                RepoManipulation.authorsOrderedByCommitsCount repo option.None
                      |> should equal "toto"
            
            
       [<Test>] member x.
     ``First CommiterAverageLinesAddedDelete.`` ()=
                RepoManipulation.authorsOrderedByAvgLinesAddedLinesDeleted repo (Some(1000))
                      |> should equal "oim"
             
