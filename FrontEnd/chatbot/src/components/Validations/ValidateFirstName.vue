<template>
    <div>
        <div class="error" v-if="!v.form.FirstName.required">Введите поле</div>
        <div class="error" v-if="!v.form.FirstName.alpha">Допускаются только буквы</div>
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