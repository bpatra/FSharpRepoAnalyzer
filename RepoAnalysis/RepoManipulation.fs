namespace RepoAnalysis
open LibGit2Sharp

module RepoManipulation =

type RepoManager(path:string) = 
    member this.GetRepo() =
        new Repository(path) //TODO: implement IDisposable...

    
    
let authorsOrderedByCommitsCount (repo:RepoManager) maxNumberOfCommits = 
    repo.GetRepo().Commits
    |> Seq.take maxNumberOfCommits 
    |> Seq.groupBy (fun(commit) -> commit.Author)
    |> Seq.sortBy (fun(group) -> 
                        snd group
                        |> Seq.length
                    )
                    

                    