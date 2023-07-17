<template>
  <div class="container">
  <h2>CRUD de Artículos</h2>

  <div class="btn-group">
    <button class="btn btn-primary" data-toggle="modal" data-target="#crearModal">Crear</button>
  </div>

  <table class="table">
    <thead class="thead-dark">
      <tr>
        <th>ID</th>
        <th>Título</th>
        <th>Descripción</th>
        <th>Acciones</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="(item, index) in listPelis" :key="index">
        <td>{{ item.articuloId }}</td>
        <td>{{ item.titulo }}</td>
        <td>{{ item.descripcion }}</td>
        <td>
          <button class="btn btn-info btn-sm" data-toggle="modal" data-target="#editarModal" v-on:click="pasarDatos(item)">Editar</button>
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
            <div class="form-group">
              <label for="categoriaAgreg">Categoria:</label>
              <input type="text" class="form-control" id="categoriaAgreg">
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
  const URL = "https://localhost:7078/api/articulos";
  export default {
    name: "comp-peli",
    data() {
      return {
        listPelis: [],
        loading: false,
      };
    },
    methods: {
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
          .post(URL, pelicula)
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
        this.loading = true;
        axios
          .delete(URL +"/"+ id)
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
        axios.patch(URL + "/" + id, pelicula).then(()=> {
          this.obtenerPelis();
          this.loading = false
        }).catch(() => this.loading = false);
      },

      obtenerPelis() {
        this.loading = true;
        axios.get(URL).then((response) => {
          this.listPelis = response.data;
          this.loading = false;
        }).catch(() => this.loading = false);
      },

      pasarDatos(item){
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
    },
    created: function () {
      this.obtenerPelis();
    },
  };
</script>

<style scoped>

</style>