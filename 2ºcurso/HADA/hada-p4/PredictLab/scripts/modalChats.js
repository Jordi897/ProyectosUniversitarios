const Modal = document.getElementById('Modal')
const searchBox = document.getElementById('searchBox')

Modal.addEventListener('shown.bs.modal', () => {
    searchBox.focus()
})