module Tests

open System
open Xunit

type MyFixture() =
    // setup
    let x = [|0|]
    do printfn "Array initialized from fixture: %A" x

    interface IDisposable with
        member this.Dispose() =
            do printfn "Clean up: %A" x

    member this.getX () = x

[<CollectionDefinition("my collection")>]
type MyCollection () =
    interface ICollectionFixture<MyFixture>

[<Collection("my collection")>]
type MyTest (fixture: MyFixture) =
    let x = fixture.getX()
    do printfn "Initialized from class constructor"

    interface IDisposable with
        member this.Dispose() =
            do printfn "Cleaning up test specific"

    [<Fact>]
    member this.``My test`` () =
        // action
        x.[0] <- 1

        // assertion
        Assert.Equal<int array>(x, [|1|])

    [<Fact>]
    member this.``My test 2`` () =
        // action
        x.[0] <- 2

        // assertion
        Assert.Equal<int array>(x, [|2|])

[<Collection("my collection")>]
type MyTest2 (fixture: MyFixture) =
    let x = fixture.getX()
    do printfn "Initialized from class constructor"

    interface IDisposable with
        member this.Dispose() =
            do printfn "Cleaning up test specific"

    [<Fact>]
    member this.``My test`` () =
        // action
        x.[0] <- 1

        // assertion
        Assert.Equal<int array>(x, [|1|])

    [<Fact>]
    member this.``My test 2`` () =
        // action
        x.[0] <- 2

        // assertion
        Assert.Equal<int array>(x, [|2|])
