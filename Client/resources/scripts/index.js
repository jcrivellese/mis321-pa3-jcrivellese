let songs = []
const url = "https://localhost:7275/api/song/" //url for the api
let urlID = ""//variable to hold the edited url for PUT/DELETE functions

async function getSongs(){//get songs function
    const response = await fetch(url)
    const data = await response.json()
    songs = data
}

function handleOnLoad(){//handle loading the page
    getSongs().then(function(f){
        songs.sort(function(a,b){//sort the array of songs by date added in descending order
            return new Date(b.dateAdded) - new Date(a.dateAdded)
        })
        populateSongTable()//load the table
    })
}

document.querySelector("#addButton").addEventListener("click", function(e){  //when the add button is clicked 
    e.preventDefault()
    document.getElementById("overlay").style.display = "block"//show overlay and popUp
    document.getElementById("addPopUp").style.display = "block"
})

document.querySelector("#addSubmit").addEventListener("click", () => {//when the submit button is clicked on the popUp
    let data = {//set data equal to the inputted values
        title: document.getElementById("titleInput").value,
        artist: document.getElementById("artistInput").value
    }
    postSong(data)//run the POST with the inputted values
})

document.querySelector("#closeAdd").addEventListener("click", () =>{//add an event listener to the close button on the add song pop up
    document.getElementById("overlay").style.display = "none"//get rid of the overlay
    document.getElementById("addPopUp").style.display = "none"
    document.getElementById("titleInput").value = ""
    document.getElementById("artistInput").value = ""
})

function postSong(data){//handle the adding of a song
    fetch(url, {//fetch the url and then run the POST method in the back end
        method: "POST",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    }).then((response) => {//afterwards get rid of the popUp and reload the table
        console.log(response)
        document.getElementById("overlay").style.display = "none"
        document.getElementById("addPopUp").style.display = "none"
        document.getElementById("titleInput").value = ""
        document.getElementById("artistInput").value = ""
        handleOnLoad()
    })
}

function handleDelete(id){//handle the delete of a song
    fetch(urlID, {//fetch the api and then run the DELETE method in the backend
        method: "DELETE",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(id)
    }).then((response) => {
        console.log(response)// afterwards reload the page
        handleOnLoad()
    })
}

function handleLike(id){//handle the like of a song
    const index = songs.findIndex(song => song.id === id)//find the index to keep the song the same=
    songs[index].favorited = !songs[index].favorited//change only the favorited
    const data = {//create a new object that will keep the same title and artist but change favorited
        title: songs[index].title,
        artist: songs[index].artist,
        favorited: songs[index].favorited
    }
    fetch(urlID, {//fetch the api and run the put method with the data
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    }).then((response) => {// afterwards reload the page
        handleOnLoad()
    })
}

document.querySelector("#editSubmit").addEventListener("click", () => {//event listener for popUp submit when editing a song, moved out of handleEdit() so that it will not add multiple event listeners
    const data = {//create a new data object with the new title and artist added
        title: document.getElementById("titleEdit").value,
        artist: document.getElementById("artistEdit").value
    }
    handleEdit(data)//run the put with the new object
})

document.querySelector("#closeEdit").addEventListener("click", () =>{//add an event listener to the close button on the edit pop up
    document.getElementById("overlay").style.display = "none"//get rid of the overlay
    document.getElementById("editPopUp").style.display = "none"//hide popUp
    document.getElementById("titleEdit").value = " "
	document.getElementById("artistEdit").value = " "
})

function handleEdit(data){//run the put in the back end 
    fetch(urlID, {//fetch the api and run the put method
        method: "PUT",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    }).then((response) => {//when gotten response
        document.getElementById("overlay").style.display = "none"//get rid of the overlay
        document.getElementById("editPopUp").style.display = "none"//hide popUp
        document.getElementById("titleEdit").value = " "
	    document.getElementById("artistEdit").value = " "
        handleOnLoad()//reload table
    })
}

function populateSongTable(){//create table of songs
    document.getElementById("songs").innerHTML = ""//set html to blank
    let table = document.createElement("table")// create the table element
    table.classList.add("table", "table-bordered","table-danger", "tableSpacing")//style the table element with bootstrap and index.css
    let thead = table.createTHead()//create the table head

    let row = thead.insertRow()//create a new row
    let th = document.createElement("th")
    let text = document.createTextNode("Song Title")//create the title header

    th.appendChild(text)
    row.appendChild(th)//add the title header to the table

    let th2 = document.createElement("th")
    let text2 = document.createTextNode("Song Artist")//create the artist header

    th2.appendChild(text2)
    row.appendChild(th2)//add the artist header to the table

    let th3 = document.createElement("th")
    let text3 = document.createTextNode(`Date Added`)//create the date added header

    th3.appendChild(text3)
    row.appendChild(th3)//add the date added header to the table

    let th4 = document.createElement("th")
    let text4 = document.createTextNode(`Favorited`)//create the favorited header

    th4.appendChild(text4)
    row.appendChild(th4)//add the favorited header

    let th5 = document.createElement("th")
    let text5 = document.createTextNode(`Functions`)//create the favorited header

    th5.appendChild(text5)
    row.appendChild(th5)

    
    songs.forEach(function(song){//for each song
        if(song.deleted == false){//if deleted is false
            let dataRow = thead.insertRow()//create a new row in the table
            let td = document.createElement("td")
            let text = document.createTextNode(`${song.title}`)

            td.appendChild(text)
            dataRow.appendChild(td)//add song title to the row

            let td2 = document.createElement("td")
            let text2 = document.createTextNode(`${song.artist}`)

            td2.appendChild(text2)
            dataRow.appendChild(td2)//add song artist to the row

            let td3 = document.createElement("td")
            let text3 = document.createTextNode(`${song.dateAdded}`)

            td3.appendChild(text3)
            dataRow.appendChild(td3)//add date added to the row

            let td4 = document.createElement("td")
            let f = song.favorited ? "Yes" : "No"//return yes if true and no if false
            let text4 = document.createTextNode(`${f}`)

            td4.appendChild(text4)
            dataRow.appendChild(td4)//add favorited status to the row

            const favoriteButton = document.createElement("button")//create the favorite button on each row
            if(song.favorited === true){
                favoriteButton.classList.add("btn", "btn-success", "btn-lg", "button")//style the button with bootstrap and index.css
            }else{
                favoriteButton.classList.add("btn", "btn-secondary", "btn-lg", "button")
            }
            favoriteButton.innerText = "Favorite"
            let td5 = document.createElement("td")
            td5.appendChild(favoriteButton)
            

            favoriteButton.addEventListener("click", () => {//when favorite button is clicked
                urlID = url + song.id//change the urlID to the put method url for the song to be liked
                handleLike(song.id)//run the like method
            })

            const editButton = document.createElement("button")//create edit button in each row
            editButton.classList.add("btn", "btn-warning", "btn-lg", "button")//style the edit button using bootstrap and index.css
            editButton.innerText = "Edit"
            let td7 = document.createElement("td")
            td5.appendChild(editButton)

            editButton.addEventListener("click", () => {//when the edit button is clicked
                document.getElementById("overlay").style.display = "block"//show overlay and popUp
                document.getElementById("editPopUp").style.display = "block"
                urlID = url + song.id//change the urlID to the put method url for the song to be edited
            })

            const deleteButton = document.createElement("button")//create delete button in each row
            deleteButton.classList.add("btn", "btn-danger", "btn-lg", "button")//style the delete button using bootstrap and index.css
            deleteButton.innerText = "Delete"
            let td6 = document.createElement("td")
            td5.appendChild(deleteButton)
            
            deleteButton.addEventListener("click", () => {//when delete button clicked
                urlID = url + song.id//change the urlID to the put method url for the song to be deleted
                handleDelete(song.id)//run the delete method
            })
            dataRow.appendChild(td5)
        }
    })

    document.getElementById("songs").appendChild(table)//append the table to the page
}


