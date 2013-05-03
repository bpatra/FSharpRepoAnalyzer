namespace RepoAnalysis
open LibGit2Sharp

module RepoManipulation =

    type public RepoManager(path:string) = 
        member this.GetRepo() =
            new Repository(path) //TODO: implement IDisposable...

    
    let minus x = -x
    
    let seqSafeTakeFilter maxNumbers sequencesToTake=  match maxNumbers with
                                                            | Some(maxNumbers) -> Seq.take maxNumbers sequencesToTake
                                                            | None -> sequencesToTake
    let authorsGrouped (repo:RepoManager) (maxNumberOfCommits:option<int>) = 
        let seqSafeTake = seqSafeTakeFilter maxNumberOfCommits
        repo.GetRepo().Commits
        |> seqSafeTake 
        |> Seq.groupBy (fun(commit) -> commit.Author.Name)
       

    let authorsOrderedByCommitsCount (repo:RepoManager) (maxNumberOfCommits:option<int>) = 
        authorsGrouped repo maxNumberOfCommits
        |> Seq.sortBy (fun(group) -> 
                            snd group
                            |> Seq.length
                            |> minus
                        )
         |> Seq.head
         |> fst

    let parentLinearHistory (commit:Commit)= 
        match Seq.isEmpty commit.Parents with
            | true -> option<Commit>.None
            | _ -> Some(Seq.head commit.Parents)

    let authorsOrderedByAvgLinesAddedLinesDeleted (repo:RepoManager) (maxNumberOfCommits:option<int>) =  
        let libGit2SharpRepo = repo.GetRepo()
        
        authorsGrouped repo maxNumberOfCommits
        |> Seq.sortBy (fun(group) -> 
                            snd group
                            |> Seq.filter  (fun(commit) -> 
                                                let parent = parentLinearHistory commit
                                                match parent with
                                                | None -> false
                                                | _ -> true
                                            )
                            |> Seq.averageBy (fun(commit) -> 
                                                let parent = parentLinearHistory commit
                                                let diff = libGit2SharpRepo.Diff.Compare(parent.Value.Tree, commit.Tree)
                                                match diff.LinesDeleted with
                                                | 0 -> 1.0
                                                | _ -> (float) (diff.LinesAdded /diff.LinesDeleted)
                                            )

                        )


                    