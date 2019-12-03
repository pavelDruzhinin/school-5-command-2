<template>
    <div>
        <div class="error" v-if="!v.form.LastName.required">Введите поле</div>
        <div class="error" v-if="!v.form.LastName.alpha">Допускаются только буквы</div>
    </div>
</template>
<script>
export default {
    props:{
        v:{
            type:Object,
            required:true
        }
    }
}
</script>