// const http = require("http");

const request = require("request")
const fs = require("fs");

let url = "http://localhost:8080/endpoint/"

let file = fs.createWriteStream("./reg/poo.png");


downloadPNG = () => {
    return new Promise((resolve, reject) => {
        request({
            uri : url,
            gzip: true
        })
        .pipe(file)
        .on('finish', async () => {
            resolve();
            console.log("The file finished downloading.");
        })
        .on('error', (error) => {
            reject(error);
            console.log("Hello");
        })
    });
}



// const { create } =  require("ipfs-http-client");
// http.get("http://localhost:8080/endpoint/", (response) => {
//     let data = '';

//     response.on('data', (chunk) => {
//         data += chunk;  
//     });


//     response.on('end', () => {
//         console.log(JSON.parse(data));
//     })

//     response.on('error', (err) => console.log(err) )
// })


// const client = ipfsHttpClient('')

