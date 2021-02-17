import * as React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import SchemaEditorApp from '@altinn/schema-editor/SchemaEditorApp';
import { deleteDataModel, fetchDataModel, newDataModel, saveDataModel, setDataModelName } from '../dataModelingSlice';
import { SchemaSelect } from '../schemaSelect';
import { Button, createStyles, Grid, makeStyles } from '@material-ui/core';
import { AddCircleOutline, DeleteOutline } from '@material-ui/icons';
function getDataModelTypeName(applicationMetadata: any) {
  if (!applicationMetadata || !applicationMetadata.dataTypes) return undefined;
  const dataTypeWithLogic = applicationMetadata.dataTypes.find((dataType: any) => dataType.appLogic);
  return dataTypeWithLogic?.id ?? 'default';
}

const useStyles = makeStyles(
  createStyles({
    root: {
      marginTop: 24,
      marginLeft: 80,
    },
    schema: {
      marginTop: 4
    },
    button: {
      margin: 4
    }
  }),
);

function DataModelingContainer(): JSX.Element {
  const classes = useStyles();
  const dispatch = useDispatch();
  const jsonSchema = useSelector((state: IServiceDevelopmentState) => state.dataModeling.schema);
  const dataModelName = useSelector(
    (state: IServiceDevelopmentState) => getDataModelTypeName(state.applicationMetadataState.applicationMetadata),
  );
  const selectedDataModelName = useSelector((state: IServiceDevelopmentState) => state.dataModeling.modelName);

  const fetchModel = (name: string) => {
    dispatch(setDataModelName({ modelName: name }));
    dispatch(fetchDataModel({}));
  }

  React.useEffect(() => {
    if (dataModelName) {
      fetchModel(dataModelName);
    }
  }, [dispatch, dataModelName]);

  const onSchemaSelected = (id: string, schema: any) => {
    fetchModel(schema.id);
  }
  const onSaveSchema = (schema: any) => {
    dispatch(saveDataModel({ schema }));
  };
  const onCreateClick = () => {
    // todo: show modal with input for name
    dispatch(newDataModel({ modelName: 'test' }));
  }
  const onDeleteClick = () => {
    console.log('delete')
    // TODO: confirmation
    dispatch(deleteDataModel({}));
    //TODO: have to fetch app meta data again
  }

  return (
    <div className={classes.root}>
      <Grid container>
        <Grid item>
          <Button
            variant="contained"
            className={classes.button}
            startIcon={<AddCircleOutline />}
            onClick={onCreateClick}
            >
          New
        </Button>
        </Grid>
        <Grid item xs={9}>
          <SchemaSelect id="schema" value={selectedDataModelName} onChange={onSchemaSelected}/>
        </Grid>
        <Grid item>
          <Button
            variant="contained"
            className={classes.button}
            startIcon={<DeleteOutline />}
            onClick={onDeleteClick}
          >
            Delete
          </Button>
        </Grid>
      </Grid>
      <SchemaEditorApp
        schema={jsonSchema || {}}
        onSaveSchema={onSaveSchema}
        rootItemId={`#/definitions/${selectedDataModelName}`}
      />
    </div>
  );
}

export default DataModelingContainer;
