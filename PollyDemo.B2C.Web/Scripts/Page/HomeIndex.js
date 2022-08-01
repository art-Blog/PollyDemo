const getHandlerLocalRetry = async () => {
    let url = "/api/Retry"
    log("get-logs", url, await $.ajax({url: url, method: "GET"}))
}
const getHandlerLocalDownGrade = async () => {
    let url = "/api/DownGrade"
    log("get-logs", url, await $.ajax({url: url, method: "GET"}))
}

const postHandlerLocalFusing = async () => {
    let url = "/api/Fusing"
    log("logs", url, await $.ajax({url: url, method: "POST", data: {Name: "michael"}}))
}

const postHandlerLocalRateLimit = async () => {
    let url = "/api/RateLimit"
    log("rate-logs", url, await $.ajax({url: url, method: "POST"}))
}

    const log = (el, target, str) => {
    let t = new Date().toISOString()
    let head = `<span style="color:red">[</span>${t}<span style="color:red">]</span>`
    let msg = `<span style="background: grey;color:lawngreen">${str}</span>`
    let message = `<li>${head}:${target} - ${msg}</li>`
    $(`#${el}`).append(message)
}


$("#btn-get-local-retry").on('click', getHandlerLocalRetry)
$("#btn-get-local-down-grade").on('click', getHandlerLocalDownGrade)

$("#btn-post-local-fusing").on('click', postHandlerLocalFusing)

$("#btn-rate-limit").on('click', postHandlerLocalRateLimit)