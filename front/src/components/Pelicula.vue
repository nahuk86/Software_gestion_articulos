<template>
  <div class="container mt-5">
    <h2>CRUD de Artículos</h2>

    <div class="row">
      <div class="col-lg-6 mb-3">
        <input class="form-control" type="text" v-model="searchValue" @input="filterTable" placeholder="Search...">
      </div>
      <div class="col-lg-2 mb-3">
        <div class="dropdown">
          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
            Categorias
          </button>
          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
            <button class="dropdown-item" v-on:click="obtenerPelis()">Todos</button>
            <button class="dropdown-item" v-for="(item, index) in listCateg" :key="index" v-on:click="obtenerPelisPorCateg(item.id)">{{ item.nombre }}</button>
          </div>
        </div>
      </div>
      <div class="col-lg-2 mb-3">
        <div class="btn-group">
          <button class="btn btn-primary px-3" data-toggle="modal" data-target="#crearModal" @click="openCrearSection">Crear Pelicula</button>
        </div>
      </div>
      <div class="col-lg-2 mb-3">
        <div class="btn-group">
          <button class="btn btn-primary" data-toggle="modal" data-target="#crearModalCateg" @click="openCrearSection">Crear Categoria</button>
        </div>
      </div>
    </div>
    <table class="table">
      <thead class="thead-dark">
        <tr>
          <th>ID</th>
          <th>Título</th>
          <th>Descripción</th>
          <th>Categoria</th>
          <th>Acciones</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(item, index) in filteredItems" :key="index">
          <td>{{ item.articuloId }}</td>
          <td>{{ item.titulo }}</td>
          <td>{{ item.descripcion }}</td>
          <td>{{ getNombreCategoria(item.categoriaId) }}</td>
          <td>
            <button class="btn btn-info btn-sm mr-2" data-toggle="modal" data-target="#editarModal" v-on:click="pasarDatos(item)">Editar</button>
            <button class="btn btn-danger btn-sm" v-on:click="eliminarPeli(item.articuloId)">Eliminar</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- Modal de Crear -->
    <div class="modal fade" id="crearModal">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Crear Artículo</h5>
            <button type="button" class="close" data-dismiss="modal">&times;</button>
          </div>
          <div class="modal-body">
            <form>
              <div class="form-group">
                <label for="tituloAgreg">Título:</label>
                <input type="text" class="form-control" id="tituloAgreg">
              </div>
              <div class="form-group">
                <label for="descripcionAgreg">Descripción:</label>
                <textarea class="form-control" id="descripcionAgreg" rows="3"></textarea>
              </div>
              <div class="dropdown">
                <label for="categoriaAgreg">Categoria:</label>
                <select class="form-control mb-3" id="categoriaAgreg" aria-label="Default select example">
                  <option v-for="(item, index) in listCateg" :key="index" v-bind:value='item.id'>{{ item.nombre }}</option>
                </select>
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" v-on:click="agregarPeli()">Guardar</button>
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
          </div>
        </div>
      </div>
    </div>

    <!-- Modal de Editar -->
    <div class="modal fade" id="editarModal">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Editar Artículo</h5>
            <button type="button" class="close" data-dismiss="modal">&times;</button>
          </div>
          <div class="modal-body">
            <form>
              <div class="form-group">
                <label for="tituloEdit">Título:</label>
                <input type="text" class="form-control" id="tituloEdit" value="">
              </div>
              <div class="form-group">
                <label for="descripcionEdit">Descripción:</label>
                <textarea class="form-control" id="descripcionEdit" rows="3"></textarea>
              </div>
              <div class="form-group">
                <label for="categoriaEdit">Categoria:</label>
                <input type="text" class="form-control" id="categoriaEdit">
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" v-on:click="editarPeli()">Guardar Cambios</button>
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
          </div>
        </div>
      </div>
    </div>

    <!-- Modal de CrearCategoria -->
    <div class="modal fade" id="crearModalCateg">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Crear Categoria</h5>
            <button type="button" class="close" data-dismiss="modal">&times;</button>
          </div>
          <div class="modal-body">
            <form>
              <div class="form-group">
                <label for="nombreAgreg">Nombre:</label>
                <input type="text" class="form-control" id="nombreAgreg">
              </div>
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary" v-on:click="agregarCateg()">Guardar</button>
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
var pelicula = {
  "articuloId": 0,
  "titulo": "",
  "descripcion": "",
  "fechaCreacion": new Date("2023-01-01T00:00:00"),
  "categoriaId": 0
};
var categoria = {
  "id": 0,
  "nombre": ""
};
const URLPeli = "https://localhost:7078/api/articulos";
const URLCateg = "https://localhost:7078/api/categorias";
export default {
  name: "comp-peli",
  data() {
    return {
      listPelis: [],
      listCateg: [],
      searchValue: '',
      loading: false,
    };
  },
  computed: {
    filteredItems() {
      if (!this.searchValue) {
        return this.listPelis;
      }
      const searchTerm = this.searchValue.toLowerCase();
      return this.listPelis.filter((item) =>
        item.articuloId.toString().toLowerCase().includes(searchTerm) ||
        item.titulo.toLowerCase().includes(searchTerm) ||
        item.descripcion.toLowerCase().includes(searchTerm)
      );
    },
  },
  methods: {
    openCrearSection() {
      // Empty the input contents
      this.$nextTick(() => {
        document.getElementById('tituloAgreg').value = '';
        document.getElementById('descripcionAgreg').value = '';
      });
    },
    filterTable() {
      const searchTerm = this.searchValue.toLowerCase();
      const tableRows = document.querySelectorAll("#Table tr");

      tableRows.forEach((row) => {
        const rowText = row.textContent.toLowerCase();
        row.style.display = rowText.includes(searchTerm) ? "" : "none";
      });
    },

    agregarPeli() {
      pelicula = {
        "articuloId": 0,
        "titulo": String(document.getElementById('tituloAgreg').value),
        "descripcion": String(document.getElementById('descripcionAgreg').value),
        "fechaCreacion": new Date(),
        "categoriaId": parseInt(document.getElementById('categoriaAgreg').value)
      };
      console.log(pelicula)
      this.loading = true;
      axios
        .post(URLPeli, pelicula)
        .then((response) => {
          console.log(response);
          this.loading = false;
          this.obtenerPelis();
        })
        .catch((error) => {
          console.error(error);
          this.loading = false;
        });
      this.tarea = "";
    },

    eliminarPeli(id) {
      console.log(id);
      this.loading = true;
      axios
        .delete(URLPeli + "/" + id)
        .then((response) => {
          console.log(response);
          this.obtenerPelis();
          this.loading = false;
        })
        .catch((error) => {
          console.log(error);
          this.loading = false;
        });
    },

    editarPeli() {
      this.loading = true;
      var id = pelicula.articuloId;
      pelicula = {
        "articuloId": id,
        "titulo": document.getElementById('tituloEdit').value,
        "descripcion": document.getElementById('descripcionEdit').value,
        "fechaCreacion": new Date(),
        "categoriaId": parseInt(document.getElementById('categoriaEdit').value)
      };
      console.log(pelicula);
      axios.patch(URLPeli + "/" + id, pelicula).then(() => {
        this.obtenerPelis();
        this.loading = false
      }).catch(() => this.loading = false);
    },

    obtenerPelis() {
      this.loading = true;
      axios.get(URLPeli).then((response) => {
        this.listPelis = response.data;
        this.loading = false;
      }).catch(() => this.loading = false);
    },
    obtenerPelisPorCateg(id) {
      this.loading = true;
      axios.get(URLPeli + "/GetArticulosPorCategoria/" + id).then((response) => {
        this.listPelis = response.data;
        this.loading = false;
      }).catch(() => this.loading = false);
    },
    pasarDatos(item) {
      pelicula = {
        "articuloId": item.articuloId,
        "titulo": item.titulo,
        "descripcion": item.descripcion,
        "fechaCreacion": Date(item.fechaCreacion),
        "categoriaId": item.categoriaId
      };
      document.getElementById('tituloEdit').value = item.titulo;
      document.getElementById('descripcionEdit').value = item.descripcion;
      document.getElementById('categoriaEdit').value = item.categoriaId;
    },
    obtenerCateg() {
      this.loading = true;
      axios.get(URLCateg).then((response) => {
        this.listCateg = response.data;
        this.loading = false;
      }).catch(() => this.loading = false);
    },
    getNombreCategoria(categoriaId) {
      const category = this.listCateg.find((c) => c.id === categoriaId);
      return category ? category.nombre : '';
    },
    agregarCateg() {
      categoria = {
        "id": 0,
        "nombre": String(document.getElementById('nombreAgreg').value),
      };
      console.log(categoria)
      this.loading = true;
      axios
        .post(URLCateg, categoria)
        .then((response) => {
          console.log(response);
          this.loading = false;
          this.obtenerCateg();
        })
        .catch((error) => {
          console.error(error);
          this.loading = false;
        });
      this.tarea = "";
    },
  },
  created: function () {
    this.obtenerPelis();
    this.obtenerCateg();
  },
};
</script>

<style scoped>

</style>