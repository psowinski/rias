namespace Rias.Common
open PromiseImpl

[<RequireQualifiedAccess>]
module Result =
    let okValue = (function
                 | Ok v -> v
                 | Error _ -> failwith "There is no Ok value.")

    let errorValue = (function
                    | Ok _ -> failwith "There is no Error value."
                    | Error v -> v)

    let bind1of2 f x y =
        match x with
        | Ok arg -> f arg y
        | Error arg -> Error arg

    let leave seq =
        let unpack resAcc x =
            match resAcc with
            | Ok acc -> match x with
                        | Ok ov -> ov::acc |> Ok
                        | Error ev -> Error ev
            | e -> e
      
        seq |> Seq.fold unpack (Ok [])
            |> Result.map List.rev

    let promiseMap mapping result = promise {
        let! x = result
        return Result.map mapping x
    }

    let promiseBind binder result = promise {
        let! x = result
        return Result.bind binder x
    }

    let promiseBind1of2 f x y = promise {
        let! x = x
        return bind1of2 f x y
    }

[<RequireQualifiedAccess>]
module PromiseResult =
    let bind f x = promise {
        let! x = x
        match x with
        | Ok ov -> return! f ov
        | Error ev -> return Error ev
    }

    let map f x = promise {
        let! x = x
        match x with
        | Ok ov ->
            let! fResult = f ov 
            return Ok fResult
        | Error ev ->
            return Error ev
    }


[<AutoOpen>]
module ResultBuilder =

    type ResultBuilder() =
        member this.Bind(x, f) = Result.bind f x
        member this.Return(x) = Ok x

    let result = ResultBuilder()
