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

    // let leave list =
    //   let rec unpack acc list =
    //     match list with
    //     | [] -> List.rev acc |> Ok
    //     | x::xs -> match x with
    //                | Ok ov -> unpack (ov::acc) xs
    //                | Error ev -> Error ev
    //   unpack [] list

    let leave seq =
      let unpack resAcc x =
        match resAcc with
        | Ok acc -> match x with
                    | Ok ov -> ov::acc |> Ok
                    | Error ev -> Error ev
        | e -> e
      
      seq |> Seq.fold unpack (Ok [])
          |> Result.map List.rev

[<AutoOpen>]
module ResultBuilder =

    type ResultBuilder() =
        member this.Bind(x, f) = Result.bind f x
        member this.Return(x) = Ok x

    let result = ResultBuilder()
