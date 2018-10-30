namespace Rias.Common

[<RequireQualifiedAccess>]
module Result =
    let okValue = function
    | Ok v -> v
    | Error _ -> failwith "Ther is no ok value."

    let errorValue = function
    | Ok _ -> failwith "Ther is no error value."
    | Error v -> v

    let bind1of2 f x y =
        match x with
        | Ok arg -> f arg y
        | Error arg -> Error arg    

[<AutoOpen>]
module ResultBuilder =

    type ResultBuilder() =
        member this.Bind(x, f) = Result.bind f x
        member this.Return(x) = Ok x

    let result = ResultBuilder()
