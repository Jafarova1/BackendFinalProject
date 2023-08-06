function setList(results){
    

    for (const person of results){
        const resultItem = document.createElement('li')

        resultItem.classList.add('result-item')


        const text = document.createTextNode(person.name)


        resultItem.appendChild(text)


        list.appendChild(resultItem)
    }
}
const searchInput = document.querySelector('.input')
searchInput.addEventListener("input", (e) => {
    e.preventDefault();
    const item = [
        { name: 'waffle'},
        { name: 'cookie'},
        { name: 'pancake'},
        { name: 'tasty'},
        { name: 'love'},
        { name: 'delicious'},
        { name: 'travel'},
        { name: 'tour'},
        { name: 'drinks'},
        { name: 'destination'},
        { name: 'food'},

    ]

    let value = e.target.value

    if (value && value.trim().length > 0){
         value = value.trim().toLowerCase()
         


        setList(item.filter(item => {
            return item.name.includes(value)

            
        }))}})
        