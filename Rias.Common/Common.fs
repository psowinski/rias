namespace Rias.Common

[<AutoOpen>]
module Railway =

    let bind f x = 
        match x with
        | Ok arg -> f arg
        | Error arg -> Error arg

    let errbind f x = 
        match x with
        | Ok arg -> Ok arg
        | Error arg -> f arg

    let bind1of2 f x y =
        match x with
        | Ok arg -> f arg y
        | Error arg -> Error arg

    let map f x = 
        match x with
        | Ok arg -> f arg |> Ok
        | Error arg -> Error arg

    type ResultBuilder() =
        member this.Bind(x, f) = bind f x
        member this.Return(x) = Ok x

    let result = ResultBuilder()
