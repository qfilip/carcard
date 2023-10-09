module ExpressionUtils

let private apply fRes xRes =
    match fRes, xRes with
    | Ok f, Ok x -> Ok (f x)
    | Ok _, Error x -> Error x
    | Error f, Ok _ -> Error f
    | Error f, Error x -> Error (List.concat [f; x])


let private (<*>) = apply


let zip a b =
    let toTuple x y = (x, y)
    Ok toTuple <*> a <*> b