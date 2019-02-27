namespace Rias.Common

module Promise =
    let promise = Promise.PromiseBuilder()

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

    let pmap f x =
        Promise.map (Result.map f) x

    let pbind f x =
        Promise.map (Result.bind f) x

    let pbind1of2 f x y =
        Promise.map (fun ux -> bind1of2 f ux y) x

    let asyncbind f x =
        promise {
            let! x = x
            match x with
            | Ok ov -> return! f ov
            | Error ev -> return Error ev
        }   

[<AutoOpen>]
module ResultBuilder =

    type ResultBuilder() =
        member this.Bind(x, f) = Result.bind f x
        member this.Return(x) = Ok x

    let result = ResultBuilder()
