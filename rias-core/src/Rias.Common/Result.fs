namespace Rias.Common

[<RequireQualifiedAccess>]
module Result =
    let okValue = function
                     | Ok v -> v
                     | Error _ -> failwith "There is no Ok value."

    let errorValue = function
                        | Ok _ -> failwith "There is no Error value."
                        | Error v -> v

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

    let mapAsync mapping result = async {
      let! x = result
      return Result.map mapping x
    }

    let bindAsync binder result = async {
      let! x = result
      return Result.bind binder x
    }

[<AutoOpen>]
module ResultBuilder =

    type ResultBuilder() =
        member this.Bind(x, f) = Result.bind f x
        member this.Return(x) = Ok x

    let result = ResultBuilder()
